using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class LoginController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Auth
        public JsonResult Auth(user user)
        {
            string sql = "select * from user where 1 ";
            if (!string.IsNullOrEmpty(user.ntid)) sql += string.Format(" and ntid='{0}'", user.ntid);
            if (!string.IsNullOrEmpty(user.employeeNum)) sql += string.Format(" and employeeNum='{0}'", user.employeeNum);
            if (!string.IsNullOrEmpty(user.password)) sql += string.Format(" and password='{0}'", user.password);
            List<user> items = db.users.SqlQuery(sql).ToList();
            if (items.Count > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}