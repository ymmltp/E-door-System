using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class DepartmentController : Controller
    {
        private mysqlBasic db = new mysqlBasic();

        // GET: Department
        public JsonResult Query()
        {
            List<department> department = db.departments.ToList();
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        //POST: Department/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(department department)
        {
            try
            {
                if (!string.IsNullOrEmpty(department.department1))
                {
                    db.departments.Add(department);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200,"Add Department Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204,"No Content");
                }
            }
            catch(Exception err)
            {
                return new HttpStatusCodeResult(400,err.Message);
            }
        }

        // GET: Department/Delete
        public ActionResult Delete(department department)
        {
            try
            {
                if (!string.IsNullOrEmpty(department.department1))
                {
                    db.departments.Attach(department);
                    db.departments.Remove(department);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Department Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "No Content");
                }
            }
            catch(Exception err)
            {
                return new HttpStatusCodeResult(400,err.Message);
            }
        }
    }
}