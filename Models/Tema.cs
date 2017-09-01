namespace MontazneKuce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tema")]
    public partial class Tema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tema()
        {
            Postovi = new HashSet<Post>();
        }

        public int TemaId { get; set; }

        [Required]
        [StringLength(300)]
        public string Naziv { get; set; }

        public DateTime DatumOtvaranja { get; set; }
        public DateTime DatumPoslednjegPosta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Postovi { get; set; }
    }
}
