using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_door_System.Models
{
    public class DashboardModel
    { }

    public class TabWeeklyCount
    {
        public string senior_tab { get; set; }
        public int QTY { get; set; }
    }

    public class MonthlyStatus
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class TierCountPerTab
    {
        public string tier { get; set; }
        public string senior_tab { get; set; }
        public int? QTY { get; set; }
    }

    public class TabCount
    {
        public string name { get; set; }
        public int value { get; set; }
    }


}