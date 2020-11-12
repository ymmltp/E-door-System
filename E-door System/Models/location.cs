namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.location")]
    public partial class location
    {
        [Key]
        [Column("location")]
        [StringLength(45)]
        public string location1 { get; set; }
    }
}
