namespace MontazneKuce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int PostId { get; set; }
        
        public string KorisnikId { get; set; }

        public int TemaId { get; set; }

        [Required(ErrorMessage ="Unesite naslov.")]
        [StringLength(60, ErrorMessage ="Naslov ne sme sadrzati preko 60 karaktera.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage ="Unesite sadrzaj")]
        public string Sadrzaj { get; set; }

        public DateTime DatumPosta { get; set; }

        public DateTime DatumPoslednjeIzmene { get; set; }

        public virtual Tema Tema { get; set; }
    }
}
