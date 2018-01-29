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
using Newtonsoft.Json;

namespace DataShow.Core.Services
{
    public class JsonToNodeData
    {
        DataShowDB DataShowDB = null;
        DataTest DataTest = null;
        public JsonToNodeData() {
            DataShowDB = new DataShowDB();
            DataTest = new DataTest();
        }


        public void CreateNodeDataByJson(string jsonData,Guid TemplatePageGUID) {
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonData);
            List<DsTemplatePageNodeData> nodeList = new List<DsTemplatePageNodeData>();
            nodeList.Add(new DsTemplatePageNodeData
            {
                TemplatePageGUID = TemplatePageGUID,
                Name = "根节点",
                TemplatePageNodeDataGUID = Guid.NewGuid(),
                Code = "A",
                ParentCode = "",
                Sql = "",
                IfSqlExcutedLooped = 0,
                SqlExcutedLoopedNum = 0,
                IfEnd = 0,
                IfDataHandled = 0,
                DataHandleAssemble = "",
                DataHandleFunction = "",
                Level = 1,
                DataType = 0,
                StaticData = ""
            });
            int level = 1;
            CreateNodeDataByJson(nodeList, "A", level, jo, TemplatePageGUID);
            DataTable dt = DataTableHelper.ToDataTable<DsTemplatePageNodeData>(nodeList);

        }

        public void CreateNodeDataByJson(List<DsTemplatePageNodeData> nodeList, string ParentCode, int level,JObject ChildNode, Guid TemplatePageGUID) {
            int code = 1;
            foreach (var node in ChildNode)
            {
                if (node.Value.Type != JTokenType.Array && node.Value.Type != JTokenType.Object)
                {
                    nodeList.Add(new DsTemplatePageNodeData
                    {
                        TemplatePageGUID = TemplatePageGUID,
                        Name = node.Key,
                        TemplatePageNodeDataGUID = Guid.NewGuid(),
                        Code = ParentCode + "." + code.ToString().PadLeft(3, '0'),
                        ParentCode = ParentCode,
                        Sql = "",
                        IfSqlExcutedLooped = 0,
                        SqlExcutedLoopedNum = 0,
                        IfEnd = 1,
                        IfDataHandled = 0,
                        DataHandleAssemble = "",
                        DataHandleFunction = "",
                        Level = level,
                        DataType = 0,
                        StaticData = ""
                    });

                }
                else if (node.Value.Type == JTokenType.Array)
                {
                    var newParentCode = ParentCode + "." + code.ToString().PadLeft(3, '0');
                    nodeList.Add(new DsTemplatePageNodeData
                    {
                        TemplatePageGUID = TemplatePageGUID,
                        Name = node.Key,
                        TemplatePageNodeDataGUID = Guid.NewGuid(),
                        Code = newParentCode,
                        ParentCode = ParentCode,
                        Sql = "",
                        IfSqlExcutedLooped = 0,
                        SqlExcutedLoopedNum = 0,
                        IfEnd = 0,
                        IfDataHandled = 0,
                        DataHandleAssemble = "",
                        DataHandleFunction = "",
                        Level = level,
                        DataType = 1,
                        StaticData = ""
                    });
                    CreateNodeDataByJson(nodeList, newParentCode, level + 1, (JObject)node.Value[0], TemplatePageGUID);
                }
                else
                {
                    var newParentCode = ParentCode + "." + code.ToString().PadLeft(3, '0');
                    nodeList.Add(new DsTemplatePageNodeData
                    {
                        TemplatePageGUID = TemplatePageGUID,
                        Name = node.Key,
                        TemplatePageNodeDataGUID = Guid.NewGuid(),
                        Code = newParentCode,
                        ParentCode = ParentCode,
                        Sql = "",
                        IfSqlExcutedLooped = 0,
                        SqlExcutedLoopedNum = 0,
                        IfEnd = 0,
                        IfDataHandled = 0,
                        DataHandleAssemble = "",
                        DataHandleFunction = "",
                        Level = level,
                        DataType = 0,
                        StaticData = ""
                    });
                    CreateNodeDataByJson(nodeList, newParentCode, level + 1, (JObject)node.Value, TemplatePageGUID);
                }
                code = code + 1;
            }

        }




    }


}
