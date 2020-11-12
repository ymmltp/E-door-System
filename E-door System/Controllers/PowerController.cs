using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class PowerController : Controller
    {
        private mysqlBasic db = new mysqlBasic();
        // GET: Power
        public JsonResult Query()
        {
            List<power> power = db.powers.ToList();
            return Json(power, JsonRequestBehavior.AllowGet);
        }
    }
}