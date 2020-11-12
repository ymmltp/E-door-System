using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class DashboardController:Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Dashboard/Weekly_Conversation_Summarization?d1&d2
        public JsonResult Weekly_Conversation_Summarization(string d1, string d2)
        {
            string sql = string.Format("SELECT DISTINCT A.senior_tab, COUNT(A.senior_tab)AS QTY from demand_list A where DATE_FORMAT(A.open_time,'%Y-%m-%d') BETWEEN ('{0}') AND ('{1}') GROUP BY(A.senior_tab)",
                d1, d2);
            List<TabWeeklyCount> items = db.Database.SqlQuery<TabWeeklyCount>(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Dashboard/Monthly_conversation_status_tracking?d1&d2
        public JsonResult Monthly_conversation_status_tracking(string d1, string d2)
        {
            string sql = string.Format("SELECT DISTINCT A.`status` as name, COUNT(id)AS value from demand_list A where DATE_FORMAT(A.open_time,'%Y-%m-%d') BETWEEN ('{0}') AND ('{1}') GROUP BY(A.`status`)",
                d1, d2);
            List<MonthlyStatus> items = db.Database.SqlQuery<MonthlyStatus>(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Dashboard/Tabular_Matrix?d1&d2
        public JsonResult Tabular_Matrix(string d1, string d2)
        {
            string sql = @"SELECT C.tier_level as tier,D.tab_type as senior_tab,TMP0.QTY FROM tier C INNER JOIN first_type D LEFT JOIN 
                            (SELECT B.tier,A.senior_tab,COUNT(A.id ) AS QTY FROM demand_list A
                            INNER JOIN `user` B ON A.`from` = B.employeeNum
                            where DATE_FORMAT(A.open_time,'%Y-%m-%d') BETWEEN ('" + d1 + "') AND ('" + d2 + @"') 
                            GROUP BY B.tier,A.senior_tab) TMP0 ON TMP0.senior_tab=D.tab_type AND TMP0.tier=C.tier_level ORDER BY C.tier_level,D.tab_type";
            List<TierCountPerTab> items = db.Database.SqlQuery<TierCountPerTab>(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Dashboard/Topic_Percent_of_Tier?tier&d1&d2
        public JsonResult Topic_Percent_of_Tier(string tier,string d1, string d2)
        {
            string sql = @"select C.tab_type AS name,IFNULL(TMP.QTY,0) as value from first_type C LEFT JOIN  
                            (SELECT DISTINCT A.senior_tab AS tab, COUNT(A.id) AS QTY
                            FROM demand_list A INNER JOIN `user` B on A.`from`=B.employeeNum 
                            WHERE B.tier='" + tier + "' AND DATE_FORMAT(A.open_time, '%Y-%m-%d') BETWEEN ('" + d1 + @"') 
                            AND ('" + d2 + "') GROUP BY A.senior_tab) TMP ON C.tab_type=TMP.tab";
            List<TabCount> items = db.Database.SqlQuery<TabCount>(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
