using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_door_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 1)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }         
            else {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult Setting()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error(string errorContext) {
            ViewBag.message = errorContext;
            return View();
        }


        //Management  Administrator only
        public ActionResult Department()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 7)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult Project()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 7)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult UserPage()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 7)
                {
                    return View("User");
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult Location()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 7)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult Tab()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 7)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }


        //Demand All 
        public ActionResult DemandApply()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 1)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        public ActionResult DemandDeal()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 1)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }
        /// <summary>
        /// if power=user : employeeNum只显示自己
        /// if power=Administrator : 显示所有employeeNum
        /// </summary>
        /// <returns></returns>
        public ActionResult DemandSearch()
        {
            HttpCookie cookie = Request.Cookies["edoor-EmployeeID"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                int power = Convert.ToInt16(HttpContext.Request.Cookies["edoor-Power"].Value);
                if (power >= 1)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "Sorry, you have no access to this page...";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.message = "Sorry, Please Log in ...";
                return View("Error");
            }
        }

        //Login
        /// <summary>
        /// 太丑
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogIn2()
        {
            return View();
        }
    }
}