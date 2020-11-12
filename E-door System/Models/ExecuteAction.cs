using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_door_System.Models
{
    public class ExecuteAction:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }



    //public JsonResult IsNoAuthority() {
    //    JsonStatus status = new JsonStatus("-1", "无权访问接口，请授权");
    //    var json = new JsonResult();
    //    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
    //    json.Data = status;
    //    return json;
    //}

    public class JsonStatus {
        public string code { set; get; }
        public string error { set; get; }
        public JsonStatus() { }
        public JsonStatus(string _code, string _error)
        {
            this.code = _code;
            this.error = _error;
        }
    }


}