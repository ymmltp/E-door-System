namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.user")]
    public partial class user
    {
        public int id { get; set; }

        [StringLength(45)]
        public string displayname { get; set; }

        [StringLength(45)]
        public string ntid { get; set; }

        [StringLength(45)]
        public string employeeNum { get; set; }

        [StringLength(10)]
        public string tier { get; set; }

        [StringLength(10)]
        public string department { get; set; }

        [StringLength(10)]
        public string project { get; set; }

        [StringLength(45)]
        public string eMail { get; set; }

        [StringLength(45)]
        public string password { get; set; }

        [StringLength(45)]
        public string power { get; set; }

        [StringLength(20)]
        public string phoneNum { get; set; }
    }
}
