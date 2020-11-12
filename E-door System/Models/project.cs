namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.project")]
    public partial class project
    {
        [Key]
        [Column("project")]
        [StringLength(10)]
        public string project1 { get; set; }
    }
}
