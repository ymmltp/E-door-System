using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class ProjectController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Project
        public JsonResult Query()
        {
            List<project> project = db.projects.ToList();
            return Json(project, JsonRequestBehavior.AllowGet);
        }

        //POST: Project/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(project project)
        {
            try
            {
                if (!string.IsNullOrEmpty(project.project1))
                {
                    db.projects.Add(project);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add Project Success");
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

        // GET: Project/Delete
        public ActionResult Delete(project project)
        {
            try
            {
                if (!string.IsNullOrEmpty(project.project1))
                {
                    db.projects.Attach(project);
                    db.projects.Remove(project);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Project Success");
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