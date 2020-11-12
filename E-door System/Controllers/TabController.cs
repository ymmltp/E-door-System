using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class TabController : Controller
    {
        private mysqlBasic db = new mysqlBasic();

        // GET: First Type
        public JsonResult First_Tab_Query()
        {
            List<first_type> first_type = db.first_type.ToList();
            return Json(first_type, JsonRequestBehavior.AllowGet);
        }
        // GET: Second Tab
        public JsonResult Second_Tab_Query()
        {
            List<tab_second_type> second_type = db.tab_second_type.ToList();
            return Json(second_type, JsonRequestBehavior.AllowGet);
        }
        // GET: Second Tab
        public JsonResult Second_Tab_Query_byRange(tab_second_type tab2)
        {
            string sql = "select * from tab_second_type where 1 ";
            if (!string.IsNullOrEmpty(tab2.first_type)) sql += string.Format(" and first_type='{0}'", tab2.first_type);
            if (!string.IsNullOrEmpty(tab2.second_type)) sql += string.Format(" and second_type='{0}'", tab2.second_type);
            if (!string.IsNullOrEmpty(tab2.description)) sql += string.Format(" and description='{0}'", tab2.description);
            List<tab_second_type> items = db.tab_second_type.SqlQuery(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        // POST: Tab/First_Tab_Create
        [HttpPost]
        public ActionResult First_Tab_Create(first_type tab)
        {
            try
            {
                if (!string.IsNullOrEmpty(tab.tab_type))
                {
                    first_type item = db.first_type.Find(tab.tab_type);
                    if(item!=null)
                        return new HttpStatusCodeResult(205, "ResetContent");
                    db.first_type.Add(tab);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add First Tab Success");
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
        // POST: Tab/Second_Tab_Create
        [HttpPost]
        public ActionResult Second_Tab_Create(tab_second_type tab)
        {
            try
            {
                if (!string.IsNullOrEmpty(tab.second_type))
                {
                    tab_second_type item = db.tab_second_type.Find(tab.second_type);
                    if (item!=null)
                        return new HttpStatusCodeResult(205, "ResetContent");
                    db.tab_second_type.Add(tab);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add Second Tab Success");
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


        // GET: Tab/First_Tab_Delete
        public ActionResult First_Tab_Delete(string tab)
        {
            try
            {
                first_type item = db.first_type.Find(tab);
                if (!string.IsNullOrEmpty(item.tab_type))
                {
                    db.first_type.Attach(item);
                    db.first_type.Remove(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Tab Success");
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
        // GET: Tab/Second_Tab_Delete
        public ActionResult Second_Tab_Delete(tab_second_type tab)
        {
            try
            {
                string sql = string.Format("select * from tab_second_type where first_type='{0}' and second_type='{1}'", tab.first_type, tab.second_type);
                List<tab_second_type> items = db.tab_second_type.SqlQuery(sql).ToList();
                if (items.Count > 0)
                {
                    tab_second_type item = items[0];
                    db.tab_second_type.Attach(item);
                    db.tab_second_type.Remove(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Tab Success");
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
        // GET: Tab/Second_Tab_Delete_byRange
        public ActionResult Second_Tab_Delete_byRange(string parenttab)
        {
            if (!string.IsNullOrEmpty(parenttab))
            {
                try
                {
                    string sql = string.Format("select * from tab_second_type where first_type='{0}'", parenttab);
                    List<tab_second_type> items = db.tab_second_type.SqlQuery(sql).ToList();
                    db.tab_second_type.RemoveRange(items);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Second Tabs Success");
                }
                catch (Exception err)
                {
                    return new HttpStatusCodeResult(400, err.Message);
                }
            }
            else {
                return new HttpStatusCodeResult(204, "No Content");
            }
        }
    }
}