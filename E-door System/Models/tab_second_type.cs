namespace E_door_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("e_door.tab_second_type")]
    public partial class tab_second_type
    {
        [Required]
        [StringLength(45)]
        public string first_type { get; set; }

        [Key]
        [StringLength(45)]
        public string second_type { get; set; }

        [StringLength(255)]
        public string description { get; set; }
    }
}
