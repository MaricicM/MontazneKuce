namespace MontazneKuce.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MontazneKuceContext : DbContext
    {
        public MontazneKuceContext() : base("DefaultConnection")
        {
        }

        public virtual DbSet<Post> Postovi { get; set; }
        public virtual DbSet<Tema> Teme { get; set; }
        public virtual DbSet<Uloga> Uloge { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>()
                .HasMany(e => e.Postovi)
                .WithRequired(e => e.Tema)
                .WillCascadeOnDelete(false);
        }
    }
}
