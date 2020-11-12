namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.department")]
    public partial class department
    {
        [Key]
        [Column("department")]
        [StringLength(10)]
        public string department1 { get; set; }
    }
}
