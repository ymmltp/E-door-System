using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class LocationController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Location
        public JsonResult Query()
        {
            List<location> location = db.locations.ToList();
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        //POST: Location/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(location location)
        {
            try
            {
                if (!string.IsNullOrEmpty(location.location1))
                {
                    db.locations.Add(location);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add Location Success");
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

        // GET: Location/Delete
        public ActionResult Delete(location location)
        {
            try
            {
                if (!string.IsNullOrEmpty(location.location1))
                {
                    db.locations.Attach(location);
                    db.locations.Remove(location);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete location Success");
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