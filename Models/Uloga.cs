namespace MontazneKuce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uloga")]
    public partial class Uloga
    {
        public int UlogaID { get; set; }

        [Required]
        [StringLength(30)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleUloge { get; set; }
    }
}
