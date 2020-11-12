using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class UserController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        private ADHelper ad = new ADHelper();
        private static string domain = "corp.jabil.org";

        // GET: User/Query
        public JsonResult Query()
        {
            List<user> items = db.users.ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        // GET: User/QuerybyID?id
        public JsonResult QuerybyID(int id)
        {
            user item = db.users.Find(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        // GET: User/QuerybyRange
        public JsonResult QuerybyRange(user user)
        {
            string sql = "select * from user where 1 ";
            if (!string.IsNullOrEmpty(user.ntid)) sql += string.Format(" and ntid='{0}'", user.ntid);
            if (!string.IsNullOrEmpty(user.displayname)) sql += string.Format(" and displayname='{0}'", user.displayname);
            if (!string.IsNullOrEmpty(user.employeeNum)) sql += string.Format(" and employeeNum='{0}'", user.employeeNum);
            if (!string.IsNullOrEmpty(user.tier)) sql += string.Format(" and tier='{0}'", user.tier);
            if (!string.IsNullOrEmpty(user.department)) sql += string.Format(" and department='{0}'", user.department);
            if (!string.IsNullOrEmpty(user.project)) sql += string.Format(" and project='{0}'", user.project);
            List<user> items = db.users.SqlQuery(sql).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        // GET: User/GetUserEntity
        public JsonResult GetUserEntity(string ntid)
        {
            ad.Domain = domain;
            UserInfo userinfo = ad.GetADUserEntity(ntid);
            return Json(userinfo, JsonRequestBehavior.AllowGet);
        }


        //POST: User/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(user user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.employeeNum))
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add User Success");
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
        //POST: User/CreateLists
        [HttpPost]
        public ActionResult CreateLists(List<user> users)  //List<user>users
        {
            try
            {
                db.users.AddRange(users);
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Add User Success");
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }
        //POST: User/Edit
        [HttpPost]
        public ActionResult Edit(user user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.employeeNum))
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Change Password Success");
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
        //POST: User/Edit
        [HttpPost]
        public ActionResult ChangePassword(int id,string password,string phoneNum)
        {
            try
            {
                string sql = string.Format("update user A set A.`password`='{0}',a.phoneNum='{1}'  WHERE A.id='{2}'", password, phoneNum, id);
                db.Database.ExecuteSqlCommand(sql);
                return new HttpStatusCodeResult(200,"Change Password Success!");
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }



        // GET: User/Delete
        public ActionResult Delete(user user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.employeeNum))
                {
                    db.users.Attach(user);
                    db.users.Remove(user);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete User Success");
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
        // GET: User/Delete?id
        public ActionResult DeletebyID(int id)
        {
            try
            {
                user item = db.users.Find(id);
                if (item!=null)
                {
                    db.users.Attach(item);
                    db.users.Remove(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete User Success");
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