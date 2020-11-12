namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.power")]
    public partial class power
    {
        [Key]
        [Column("type")]
        [StringLength(20)]
        public string type { get; set; }
    }
}