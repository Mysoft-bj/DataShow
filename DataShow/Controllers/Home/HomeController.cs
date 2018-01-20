using My.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Application;
using My.Domain;
using My.Application.Login;
using My.Core.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataShow.Controllers.home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //DataTable tblDatas = new DataTable("Datas");
            //DataColumn dc = null;
            //dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            //dc.AutoIncrement = true;//自动增加
            //dc.AutoIncrementSeed = 1;//起始为1
            //dc.AutoIncrementStep = 1;//步长为1
            //dc.AllowDBNull = false;//

            //dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            //dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            //dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));

            //DataRow newRow;
            //newRow = tblDatas.NewRow();
            //newRow["Product"] = "大话西游";
            //newRow["Version"] = "2.0";
            //newRow["Description"] = "我很喜欢";
            //tblDatas.Rows.Add(newRow);

            //newRow = tblDatas.NewRow();
            //newRow["Product"] = "梦幻西游";
            //newRow["Version"] = "3.0";
            //newRow["Description"] = "比大话更幼稚";
            //tblDatas.Rows.Add(newRow);
            //var json=  DataTableHelper.Dtb2Json(tblDatas);


            string jsonText = "{\"a\": \"\", 	\"b\": \"\", 	\"c\": [{ 			\"c.1\": \"\", 			\"c.2\": \"\" 		}, 		{ 			\"c.1\": \"\", 			\"c.2\": \"\" 		} 	], 	\"d\": [{ 		\"d.1\": \"\", 		\"d.2\": [{ 			\"d.2.1\": \"\", 			\"d.2.2\": \"\" 		}] 	}] }";
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);

            return View();
        }

    }
}
