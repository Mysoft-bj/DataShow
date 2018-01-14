using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using My.Core;
using System.Web.Script.Serialization;

namespace DataShow.Controllers.Charts
{
    public class ChartsController : Controller
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 将datatable转换为json  
        /// </summary>
        /// <param name="dtb">Dt</param>
        /// <returns>JSON字符串</returns>
        public static string Dt2Json(DataTable dt)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);
            }
            //序列化  
            return json.Serialize(dic);
        }

        //获取销量排名柱状图
        public string GetSaleRanking()
        {
            var sql = string.Format(@"
                SELECT TOP 3 ProjName as name,cnt as value FROM (
                SELECT b.ProjName,COUNT(a.ContractGUID) AS cnt FROM dbo.s_Contract a
                LEFT JOIN dbo.p_Project b ON a.ProjGUID=b.ProjGUID
                WHERE a.Status='激活'
                GROUP BY b.ProjName
                ) a
                ORDER BY cnt DESC 
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取不同年龄段的购买力
        public string GetDiffAgeBuy()
        {
            var sql = string.Format(@"
                SELECT age AS name,COUNT(1) AS value FROM (
                SELECT CASE WHEN (YEAR(a.BirthDate)-1920)<80 THEN CONVERT(VARCHAR(10),(YEAR(a.BirthDate)-1900)/10)+'0后' ELSE CONVERT(VARCHAR(10),(YEAR(a.BirthDate)-2000)/10)+'0后' END AS age, 
                CASE WHEN YEAR(a.BirthDate)%100>=20 THEN YEAR(a.BirthDate)%100/10 ELSE 100 END AS ordernum
                FROM dbo.s_Trade2Cst a
                LEFT JOIN dbo.s_Contract b ON a.TradeGUID = b.TradeGUID 
                WHERE b.Status='激活' AND a.BirthDate IS NOT NULL
                ) a
                GROUP BY age,ordernum
                ORDER BY ordernum
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取最大购买年龄和最小购买年龄
        public string GetTwoDiffAge() 
        {
            var sql = string.Format(@"
                SELECT YEAR(GETDATE())- YEAR(MAX(BirthDate)) AS eldest,YEAR(GETDATE())-YEAR(MIN(BirthDate)) AS oldest
                FROM dbo.s_Trade2Cst a
                LEFT JOIN dbo.s_Contract b ON a.TradeGUID = b.TradeGUID 
                WHERE b.Status='激活' AND a.BirthDate IS NOT NULL AND YEAR(BirthDate)<>1900
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0) 
                {
                    return "[{name:'eldest',value:" + dt.Rows[0][0] + "},{name:'oldest',value:" + dt.Rows[0][1] + "}]";
                }
            }
            return "";
        }
        //获取跟进次数排名最前的5个客户及所属公司
        public string GetMostCstGjjl()
        {
            var sql = string.Format(@"
                SELECT TOP 5 BUName AS name,cnt AS value FROM (
                    SELECT b.cstguid,d.BUName,a.OppGUID,COUNT(a.GjjlGUID) AS cnt FROM dbo.s_Opp2Gjjl a
                    LEFT JOIN dbo.[s_Opp2Cst ] b ON a.OppGUID=b.OppGUID
                    LEFT JOIN dbo.p_CstAttach c ON b.CstGUID = c.CstGUID
                    LEFT JOIN dbo.myBusinessUnit d ON d.BUGUID=c.BUGUID
                    WHERE GjDate BETWEEN '2013-01-01' AND '2013-12-31'
                    GROUP BY b.cstguid,d.BUName,a.OppGUID
                    ) a
                    ORDER BY cnt DESC
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取各产品类型的成交量
        public string GetDiffProductCnt()
        {
            var sql = string.Format(@"
                SELECT c.BProductTypeName AS name,COUNT(1) AS value FROM dbo.s_Contract a
                LEFT JOIN dbo.p_Room b ON a.RoomGUID=b.RoomGUID
                LEFT JOIN dbo.p_BuildProductType c ON b.BProductTypeCode = c.BProductTypeCode
                WHERE a.Status='激活'
                GROUP BY c.BProductTypeName
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取最大的产品类型对应的成交数量,进位1000(例如4222-->5000)
        public string GetMaxProductCnt() 
        {
            var sql = string.Format(@"
                SELECT c.BProductTypeName AS name,(
                SELECT MAX(value)/1000*1000+1000 FROM (
                SELECT c.BProductTypeName AS name,COUNT(1) AS value FROM dbo.s_Contract a
                LEFT JOIN dbo.p_Room b ON a.RoomGUID=b.RoomGUID
                LEFT JOIN dbo.p_BuildProductType c ON b.BProductTypeCode = c.BProductTypeCode
                WHERE a.Status='激活'
                GROUP BY c.BProductTypeName
                ) a
                ) AS max FROM dbo.s_Contract a
                LEFT JOIN dbo.p_Room b ON a.RoomGUID=b.RoomGUID
                LEFT JOIN dbo.p_BuildProductType c ON b.BProductTypeCode = c.BProductTypeCode
                WHERE a.Status='激活'
                GROUP BY c.BProductTypeName
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取各媒体带来的接电接访
        public string GetMediaOppCnt()
        {
            var sql = string.Format(@"
                SELECT TOP 5 name,value FROM (
                SELECT MainMediaName AS name,COUNT(1) AS value FROM dbo.s_Opportunity
                WHERE ISNULL(MainMediaName,'')<>''
                GROUP BY MainMediaName
                ) a
                ORDER BY value DESC
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }

        //获取各月份成交量
        public string GetDiffMonthDealCnt()
        {
            var sql = string.Format(@"
                SELECT CONVERT(VARCHAR(200),MONTH(QSDate))+'月' AS name,COUNT(1) AS value FROM dbo.s_Contract WHERE Status='激活'
                GROUP BY MONTH(QSDate)
                ORDER BY MONTH(QSDate)
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }

        //统计整个公司平均去化率
        public string GetRemovalRate()
        {
            var sql = string.Format(@"
                SELECT '' as name,CASE WHEN SUM(a.cnt)=0 THEN 0 ELSE CAST(CAST(SUM(b.cnt) as money) *100/CAST(SUM(a.cnt) as money)  as decimal(5,2) )  END AS value FROM (
                SELECT COUNT(1) AS cnt FROM dbo.p_Room a
                ) a LEFT JOIN (
                SELECT COUNT(1) AS cnt FROM dbo.p_Room a
                INNER JOIN dbo.s_Contract b ON a.RoomGUID=b.RoomGUID
                WHERE b.Status='激活'
                ) b ON 1=1
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //统计房间价格分布
        public string GetRoomPrice()
        {
            var sql = string.Format(@"
               
SELECT * FROM (
SELECT '≥20000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price>=20000 AND Price IS NOT NULL
) b ON 1=1
UNION ALL
SELECT '≥15000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price<20000 AND Price>=15000 AND Price IS NOT NULL
) b ON 1=1
UNION ALL
SELECT '≥10000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price<15000 AND Price>=10000 AND Price IS NOT NULL
) b ON 1=1
UNION ALL
SELECT '10000元' AS name,1.00-SUM(value) AS value FROM (
SELECT '≥20000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price>=20000 AND Price IS NOT NULL
) b ON 1=1
UNION ALL
SELECT '≥15000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price<20000 AND Price>=15000 AND Price IS NOT NULL
) b ON 1=1
UNION ALL
SELECT '≥10000 元' AS name,CASE WHEN sumcnt=0 THEN 0 ELSE CAST(CAST(cnt1 AS money)/CAST(sumcnt AS money) as decimal(5,2)) END AS value FROM (
SELECT COUNT(1) AS sumcnt FROM dbo.p_Room WHERE Price IS NOT NULL
) a
LEFT JOIN (
SELECT COUNT(1) AS cnt1 FROM dbo.p_Room WHERE Price<15000 AND Price>=10000 AND Price IS NOT NULL
) b ON 1=1
) a
) a ORDER BY a.value
                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取项目落地的城市
        public string GetAchievedProjectCount()
        {
            var sql = string.Format(@"
              SELECT c.city AS name ,COUNT(c.city) AS value   FROM dbo.p_Project  AS p
INNER JOIN   p_City AS c ON p.ProjAddress LIKE '%'+ REPLACE(c.city,'市','') +'%' or p.ProjName like '%'+ REPLACE(c.city,'市','') +'%'
or p.ProjInfo like '%'+ REPLACE(c.city,'市','') +'%' or p.TargetEffigy like '%'+ REPLACE(c.city,'市','') +'%'
GROUP BY c.city

                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
        //获取购房星座分布
        public string GetConstellations()
        {
            var sql = string.Format(@"
              select name,COUNT(1) as value from (
select case when (b.BirthDate >= '12-22' and b.BirthDate<='12-31') or b.BirthDate<='01-19' then '魔羯座' 
		when b.BirthDate between '01-20' and '02-18' then '水瓶座' 
		 when b.BirthDate between '02-19' and '03-20' then '双鱼座' 
		when b.BirthDate between '03-21' and '04-20' then '白羊座' 
		when b.BirthDate between '04-21' and '05-20' then '金牛座' 
		when b.BirthDate between '05-21' and '06-21' then '双子座' 
		 when b.BirthDate between '06-22' and '07-22' then '巨蟹座' 
		when b.BirthDate between '07-23' and '08-22' then '狮子座' 
		when b.BirthDate between '08-23' and '09-22' then '处女座' 
		when b.BirthDate between '09-23' and '10-22' then '天秤座' 
		 when b.BirthDate between '10-23' and '11-21' then '天蝎座' 
		when b.BirthDate between '11-22' and '12-21' then '射手座' 
		end  as name
from s_Trade a
inner join (
	select TradeGUID,right(convert(varchar(10),BirthDate,120),5) as BirthDate  
	from dbo.s_Trade2Cst 
	where BirthDate is not null
) b on a.TradeGUID=b.TradeGUID
where a.TradeStatus='激活'
) a
group by name

                ");
            DataTable dt = SqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            return Dt2Json(dt);
        }
    }
}
