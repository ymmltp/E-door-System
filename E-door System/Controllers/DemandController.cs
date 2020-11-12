using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class DemandController:Controller
    {
        private mysqlBasic db = new mysqlBasic();

        //GET: Demand/QueryDemandbyRange
        public JsonResult QueryDemandbyRange(demand_list demand)
        {
            if (string.IsNullOrEmpty(demand.from) && string.IsNullOrEmpty(demand.to))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                string user = HttpContext.Request.Cookies["edoor-EmployeeID"].Value;
                if (power < 7)
                {
                    demand.from = user;
                    demand.to = user;
                }
                //string sql = @"SELECT A.* from demand_list A where 1 ";
                string sql = @"SELECT tmp.*,B.displayname as mailTo from  
                            (SELECT A.*,B.displayname AS mailFrom FROM demand_list A INNER JOIN `user` B ON A.`from` = B.employeeNum
                             where 1 ";
                if (!string.IsNullOrEmpty(demand.from)) sql += string.Format(" and A.from='{0}'", demand.from);
                if (!string.IsNullOrEmpty(demand.to)) sql += string.Format(" OR A.to like '%{0}%'", demand.to);
                if (!string.IsNullOrEmpty(demand.senior_tab)) sql += string.Format(" and A.senior_tab='{0}'", demand.senior_tab);
                if (!string.IsNullOrEmpty(demand.tab)) sql += string.Format(" and A.tab='{0}'", demand.tab);
                if (!string.IsNullOrEmpty(demand.status)) sql += string.Format(" and A.status='{0}'", demand.status);
                sql += " ) tmp  INNER JOIN `user` B ON tmp.`to` = B.employeeNum ORDER BY tmp.`status` DESC";
                List<MyDemand> items = db.Database.SqlQuery<MyDemand>(sql).ToList();
                //List<demand_list> items = db.demand_list.SqlQuery(sql).ToList();
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else {
                string sql = @"SELECT tmp.*,B.displayname as mailTo from  
                            (SELECT A.*,B.displayname AS mailFrom FROM demand_list A INNER JOIN `user` B ON A.`from` = B.employeeNum
                             where 1 ";
                if (!string.IsNullOrEmpty(demand.from)) sql += string.Format(" and A.from ='{0}'", demand.from);
                if (!string.IsNullOrEmpty(demand.to)) sql += string.Format(" AND A.to like '%{0}%'", demand.to);
                if (!string.IsNullOrEmpty(demand.senior_tab)) sql += string.Format(" and A.senior_tab='{0}'", demand.senior_tab);
                if (!string.IsNullOrEmpty(demand.tab)) sql += string.Format(" and A.tab='{0}'", demand.tab);
                if (!string.IsNullOrEmpty(demand.status)) sql += string.Format(" and A.status='{0}'", demand.status);
                sql += " ) tmp  INNER JOIN `user` B ON tmp.`to` = B.employeeNum ORDER BY tmp.`status` DESC";
                List<MyDemand> items = db.Database.SqlQuery<MyDemand>(sql).ToList();
                return Json(items, JsonRequestBehavior.AllowGet);
            }
        }
        //GET: Demand/QueryEnableDemand
        public JsonResult QueryEnableDemand(demand_list demand)
        {
            string sql = @"SELECT tmp.*,B.displayname as mailTo from  
                            (SELECT A.*,B.displayname AS mailFrom FROM demand_list A INNER JOIN `user` B ON A.`from` = B.employeeNum
                             where 1 ";
            if (!string.IsNullOrEmpty(demand.from)) sql += string.Format(" AND A.from='{0}'", demand.from);
            if (!string.IsNullOrEmpty(demand.to)) sql += string.Format(" AND A.to like '%{0}%'", demand.to);
            sql += " AND A.status!='close'";
            sql += " ) tmp  INNER JOIN `user` B ON tmp.`to` = B.employeeNum ORDER BY tmp.`status`";
            List<MyDemand> items = db.Database.SqlQuery<MyDemand>(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //GET: Demand/QuerybyID?id
        public JsonResult QuerybyID(int id)
        {
            demand_list item = db.demand_list.Find(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //GET: Demand/QueryIsTimeOK
        public JsonResult QueryIsTimeOK(demand_list demand)
        {
            string sql = "Select * from demand_list A where 1 ";
            sql += string.Format(" and A.to='{0}'", demand.to);
            sql += string.Format(" and A.session_time='{0}'", demand.session_time.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            List<demand_list> items = db.demand_list.SqlQuery(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);             
        }


        //POST:Edit
        public ActionResult Edit(demand_list demand)
        {
            try {
                if (!string.IsNullOrEmpty(demand.from))
                {
                    db.Entry(demand).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Arrange Demand Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "No Content");
                }
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            } 
        }


        //POST: Demand/ApplyDemand
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ApplyDemand(demand_list demand)
        {
            try
            {
                if (!string.IsNullOrEmpty(demand.from))
                {
                    db.demand_list.Add(demand);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Apply Demand Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "No Content");
                }
        }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }


        // GET: Demand/Delete?id
        public ActionResult DeletebyID(int id)
        {
            try
            {
                demand_list item = db.demand_list.Find(id);
                if (item != null)
                {
                    db.demand_list.Attach(item);
                    db.demand_list.Remove(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Demand Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "No Content");
                }
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }



    }
}