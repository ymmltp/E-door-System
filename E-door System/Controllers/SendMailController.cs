using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using E_door_System.Models;

namespace E_door_System.Controllers
{
    public class SendMailController: Controller
    {
        private mysqlBasic db = new mysqlBasic();

        //发送请求
        public ActionResult SendApplyEmail(demand_list demand) {
            try
            {
                string sql = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.to);
                List<user> mailto = db.users.SqlQuery(sql).ToList();
                string sql1 = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.from);
                List<user> mailfrom = db.users.SqlQuery(sql).ToList();
                string cont = "<table style=border-collapse:collapse border=1 bordercolor=DCDCDC ><tr align=center bgcolor=#F5F5F5><td><B>From</B></td><td><B>Topic</B></td><td><B>Tab</B></td><td><B>Content</B></td></tr>";
                cont += string.Format("<tr align=center><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", mailfrom[0].displayname, demand.senior_tab, demand.tab, demand.content);
                cont += "</table>";
                if (mailto.Count == 1)
                {
                    mailHelper.SendEmail("", mailto[0].eMail, "", "e-Door Apply", cont);
                    return new HttpStatusCodeResult(200, "Send mail Success");
                }
                else {
                    return new HttpStatusCodeResult(204, "Target Email address error" + mailto.Count);
                }
            }
            catch(Exception err) {
                return new HttpStatusCodeResult(400, err.Message);
            }            
        }


        //安排时间
        public ActionResult SendArrangeEmail(demand_list demand)
        {
            try
            {
                string sql = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.from);
                List<user> mailfrom = db.users.SqlQuery(sql).ToList();
                string sql1 = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.to);
                List<user> mailto = db.users.SqlQuery(sql).ToList();
                string cont = "Your demand about " + demand.tab + " have be arranged.</br></br>";
                cont += "<table style=border-collapse:collapse border=1 bordercolor=DCDCDC ><tr align=center bgcolor=#F5F5F5><td><B>From</B></td><td><B>Topic</B></td><td><B>Tab</B></td><td><B>Content</B></td><td><B>To</B></td><td><B>Session Time</B></td><td><B>Session Location</B></td><td><B>Session Duration(min)</B></td></tr>";
                cont += string.Format("<tr align=center><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", mailfrom[0].displayname, demand.senior_tab, demand.tab, demand.content,mailto[0].displayname,demand.session_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),demand.session_location,demand.session_duration);
                cont += "</table>";
                cont += "</br><B>Please attend this session on time.</B>";
                if (mailfrom.Count == 1)
                {
                    mailHelper.SendEmail("", mailfrom[0].eMail, "", "e-Door Answer", cont);
                    return new HttpStatusCodeResult(200, "Send mail Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "Target Email address error" + mailfrom.Count);
                }
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }


        //安排时间
        public ActionResult SendCloseEmail(demand_list demand)
        {
            try
            {
                string sql = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.from);
                List<user> mailto = db.users.SqlQuery(sql).ToList();
                string sql1 = string.Format("Select * from user A where A.employeeNum = '{0}'", demand.to);
                List<user> mailfrom = db.users.SqlQuery(sql).ToList();

                string cont = "Your demand about <B>" + demand.tab + "</B> have be Closed.</br></br>";
                cont += "<table style=border-collapse:collapse border=1 bordercolor=DCDCDC ><tr align=center bgcolor=#F5F5F5><td><B>From</B></td><td><B>Topic</B></td><td><B>Tab</B></td><td><B>Content</B></td><td><B>To</B></td><td><B>Session Time</B></td><td><B>Session Location</B></td><td><B>Remark</B></td></tr>";
                cont += string.Format("<tr align=center><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", mailfrom[0].displayname, demand.senior_tab, demand.tab, demand.content, mailto[0].displayname, demand.session_time.Value.ToString("yyyy-MM-dd HH:mm:ss"), demand.session_location,demand.remark);
                cont += "</table>";
                if (mailfrom.Count == 1)
                {
                    mailHelper.SendEmail("", mailfrom[0].eMail, "", "e-Door Closed Message", cont);
                    return new HttpStatusCodeResult(200, "Send mail Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204, "Target Email address error" + mailfrom.Count);
                }
            }
            catch (Exception err)
            {
                return new HttpStatusCodeResult(400, err.Message);
            }
        }
    }
}