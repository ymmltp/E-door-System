namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.tier")]
    public partial class tier
    {
        [Key]
        [StringLength(10)]
        public string tier_level { get; set; }
    }
}
