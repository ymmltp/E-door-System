namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.demand_list")]
    public partial class demand_list
    {
        public int id { get; set; }

        [StringLength(45)]
        public string from { get; set; }

        public DateTime? open_time { get; set; }

        [StringLength(45)]
        public string senior_tab { get; set; }

        [StringLength(45)]
        public string tab { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        [StringLength(45)]
        public string status { get; set; }

        [StringLength(45)]
        public string to { get; set; }

        [StringLength(45)]
        public string cc { get; set; }

        public DateTime? session_time { get; set; }

        [StringLength(45)]
        public string session_location { get; set; }

        [StringLength(45)]
        public string session_duration { get; set; }

        public DateTime? close_time { get; set; }

        [StringLength(255)]
        public string remark { get; set; }

        [StringLength(45)]
        public string attatchment { get; set; }
    }
}
