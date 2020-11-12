namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.first_type")]
    public partial class first_type
    {
        [Key]
        [StringLength(45)]
        public string tab_type { get; set; }

        [StringLength(45)]
        public string description { get; set; }
    }
}
