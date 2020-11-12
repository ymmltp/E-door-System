using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class TierController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Tier
        public JsonResult Query()
        {
            List<tier> tier = db.tiers.ToList();
            return Json(tier, JsonRequestBehavior.AllowGet);
        }
    }
}