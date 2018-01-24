using My.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using Newtonsoft.Json.Linq;
using DataShow.Services;
using System.Data;
using My.Core.Helper;

namespace DataShow.Core.Services
{
    public class DataShowCore
    {
        DataShowDB DataShowDB = null;
        DataTest DataTest = null;
        public DataShowCore() {
            DataShowDB = new DataShowDB();
            DataTest = new DataTest();
        }
        public void CreateDataShowAllJson(Guid EntitGUID) {

        }


        public void CreateDataShowPageJson(Guid EntityPageGUID)
        {
            //List<DsEntityPageNodeData> nodeList= DataShowDB.GetPageNodeList(EntityPageGUID);
            List<DsEntityPageNodeData> nodeList = DataTest.PageNodeList();

            //父级是sql模式取数，子级移除
            foreach (var da in nodeList.Where(node => node.DataType == 1)) {
                nodeList.RemoveAll(node => node.Code.StartsWith(da.Code + "."));
            }
            JObject json = new JObject();
            foreach (var da in nodeList.Where(node=>node.Level==2).OrderBy(node=> node.Code)) {
                if (da.IfEnd == 1 && da.DataType == 0)
                {
                    json.Add(new JProperty(da.Name, da.StaticData));
                }
                else if (da.DataType == 1) {

                    json.Add(new JProperty(da.Name, CreateDataBySQl(da)));
                }
                else
                {
                    json.Add(new JProperty(da.Name, CreateChildNodeObject(nodeList.Where(node => node.ParentCode ==da.Code ).ToList())));
                }
            }

            var a= json.ToString();

        }
        //获取子级json递归方法
        public object CreateChildNodeObject(List<DsEntityPageNodeData> childNode) {
            JObject childJson = new JObject();
            foreach (var da in childNode.OrderBy(node => node.Code)) {
                if (da.IfEnd == 1 && da.DataType == 0)
                {
                    childJson.Add(new JProperty(da.Name, da.StaticData));
                }
                else if (da.DataType == 1)
                {

                    childJson.Add(new JProperty(da.Name, CreateDataBySQl(da)));
                }
                else
                {
                    if(childNode.Where(node => node.Code.StartsWith(da.Code + ".")).Count() > 0) {
                        childJson.Add(new JProperty(da.Name, CreateChildNodeObject(childNode.Where(node => node.ParentCode == da.Code).ToList())));
                    }
                }
            }
            return childJson;
        }
        //根据sql创建json对象
        public object CreateDataBySQl(DsEntityPageNodeData node)
        {
            DataTable dt;
            if (node.IfSqlExcutedLooped == 0)
            {
                dt = DataCore(node.Sql);

                if (node.IfDataHandled == 0)
                {
                    return DataTableHelper.Dt2Object(dt);
                }
                else
                {
                    return (JObject)Invoke(node.DataHandleAssemble, node.DataHandleFunction, dt, "");
                }
            }
            else {
                var countSql = "SELECT COUNT(1) FROM (" + node.Sql + ")temp";
                var countStatue= "SELECT TOP 1 * FROM (" + node.Sql + ")temp";
                var count = Convert.ToInt32(DataCore(countSql).Rows[0][0]);
                var firstFiled = DataCore(countSql).Columns[0].ToString();
                if (count > 0)
                {
                    int page = 0;
                    var pageSql = "";
                    if (node.SqlExcutedLoopedNum == 0)
                    {
                        dt = DataCore(node.Sql);
                        return (JObject)Invoke(node.DataHandleAssemble, node.DataHandleFunction, dt, "");
                    }
                    else {
                        page = count / (int)node.SqlExcutedLoopedNum + 1;
                        pageSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + firstFiled + ") RowId_TempTable,*  FROM (" + node.Sql + ")tt )temp WHERE  RowId_TempTable BETWEEN 1 AND " + node.SqlExcutedLoopedNum.ToString();
                        dt = DataCore(pageSql);

                        if (page > 1) { 
                            for (var i = 1; i < page; i++) {
                                pageSql= "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + firstFiled + ") RowId_TempTable,*  FROM (" + node.Sql + ")tt )temp WHERE  RowId_TempTable BETWEEN "+ (node.SqlExcutedLoopedNum*i+1).ToString() + " AND " + (node.SqlExcutedLoopedNum * (i + 1)).ToString();
                                DataTable dtNew= DataCore(pageSql);
                                dt.Merge(dtNew);
                            }
                        }
                        return (JObject)Invoke(node.DataHandleAssemble, node.DataHandleFunction, dt, "");
                    }


                }
                else {
                    return "[]";
                }
            }

        }


        private object Invoke(object excute, string action, DataTable data,string tableName)
        {
            System.Reflection.MethodInfo methodInfo = null;
            try
            {
                methodInfo = excute.GetType().GetMethod(action);
                object[] parameters;
                var paraTypes = methodInfo.GetParameters();
                parameters = new object[paraTypes.Length];

                if (parameters.Length == 0)
                {
                }
                else
                {
                    parameters[0] = data;
                    parameters[1] = tableName;
                }
                var result = methodInfo.Invoke(excute, parameters);
                return result;
            }
            catch (Exception ex)
            {
                return "[]";
            }
        }

        public DataTable DataCore(string sql) {
            return new DataTable();  
        }
    }


}
