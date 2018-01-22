using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace My.Core.Helper
{
   public class DataTableHelper
    {
        /// <summary>
        /// 将datatable转换为json  
        /// </summary>
        /// <param name="dtb">Dt</param>
        /// <returns>JSON字符串</returns>
        public static string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);

            }
            //序列化  
            return jss.Serialize(dic);
        }

        public static object Dt2Object(DataTable dt) {
            JArray jsonArr = new JArray();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                JObject json = new JObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    json.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                jsonArr.Add(json);

            }
            return jsonArr;
        }
    }
}
