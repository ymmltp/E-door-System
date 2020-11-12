using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_door_System.Models
{
    public class MyDemand
    {
        public int id { get; set; }
        public string from { get; set; }
        public string mailFrom { get; set; }
        public DateTime? open_time { get; set; }
        public string senior_tab { get; set; }
        public string tab { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public string to { get; set; }
        public string mailTo { get; set; }
        public string cc { get; set; }
        public DateTime? session_time { get; set; }
        public string session_location { get; set; }
        public DateTime? close_time { get; set; }
        public string remark { get; set; }
        public string attatchment { get; set; }
        
    }
}


