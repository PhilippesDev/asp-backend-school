using api_gestion_ecole.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<AnneeScolaire> AnneeScolaire { get; set; }
        public DbSet<Classe> Classe { get; set; }
        public DbSet<Eleve> Eleve { get; set; }
        public DbSet<Option> Option { get; set; }
        public DbSet<Frais> Frais { get; set; }
        public DbSet<CategorieFrais> CategorieFrais { get; set; }
        public DbSet<Semestre> Semestre { get; set; }
        public DbSet<Periode> Periode { get; set; }
        public DbSet<Paiement> Paiement { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Enseignant> Enseignant { get; set; }
        public DbSet<Inscription> Inscription { get; set; }
        public DbSet<CoursConcernerClasse> CoursConcernerClasse { get; set; }
        public DbSet<FraisConcernerClasse> FraisConcernerClasses { get; set; }
        public DbSet<Cotation> Cotation { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Presence> Presence { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Inscription>().HasKey(i=> new {i.Id});
            builder.Entity<Inscription>().HasIndex(i=> new {i.EleveId, i.ClasseId, i.AnneeScolaireId}).IsUnique();
            builder.Entity<Inscription>().HasOne(i=>i.Eleve).WithMany(e=>e.Insciptions).HasForeignKey(i=>i.EleveId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Inscription>().HasOne(i=>i.Classe).WithMany(c=>c.Insciptions).HasForeignKey(i=>i.ClasseId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Inscription>().HasOne(i=>i.AnneeScolaire).WithMany(a=>a.Insciptions).HasForeignKey(i=>i.AnneeScolaireId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CoursConcernerClasse>().HasKey(c=>new{c.Id});
            builder.Entity<CoursConcernerClasse>().HasOne(c=>c.Classe).WithMany(c=>c.CoursConcernerClasses).HasForeignKey(c=>c.ClasseId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CoursConcernerClasse>().HasOne(c=>c.Cours).WithMany(c=>c.CoursConcernerClasses).HasForeignKey(c=>c.CoursId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CoursConcernerClasse>().HasOne(c=>c.AnneeScolaire).WithMany(a=>a.CoursConcernerClasses).HasForeignKey(c=>c.AnneeScolaireId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CoursConcernerClasse>().HasOne(c=>c.Enseignant).WithMany(e=>e.ConcernerClasses).HasForeignKey(c=>c.EnseignantId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CoursConcernerClasse>().HasIndex(c=>new{c.CoursId, c.ClasseId, c.AnneeScolaireId}).IsUnique();

            builder.Entity<FraisConcernerClasse>().HasKey(f=>new{f.Id});
            builder.Entity<FraisConcernerClasse>().HasOne(f=>f.Classe).WithMany(c=>c.FraisConcernerClasses).HasForeignKey(f=>f.ClasseId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<FraisConcernerClasse>().HasOne(f=>f.Frais).WithMany(f=>f.FraisConcernerClasses).HasForeignKey(f=>f.FraisId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<FraisConcernerClasse>().HasOne(f=>f.AnneeScolaire).WithMany(a=>a.FraisConcernerClasses).HasForeignKey(f=>f.AnneeScolaireId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<FraisConcernerClasse>().HasIndex(f=>new{f.FraisId, f.ClasseId, f.AnneeScolaireId}).IsUnique();

            builder.Entity<Cotation>().HasKey(c=>new{c.Id});
            builder.Entity<Cotation>().HasOne(c=>c.Inscription).WithMany(i=>i.Cotations).HasForeignKey(c=>c.InscriptionId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Cotation>().HasOne(c=>c.CoursConcernerClasse).WithMany(c=>c.Cotations).HasForeignKey(c=>c.CoursConcernerClasseId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Cotation>().HasOne(c=>c.Periode).WithMany(p=>p.Cotations).HasForeignKey(c=>c.PeriodeId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Cotation>().HasIndex(c=>new{c.InscriptionId, c.CoursConcernerClasseId, c.PeriodeId}).IsUnique();
        
            builder.Entity<AnneeScolaire>().HasIndex(a=> new {a.Designation}).IsUnique();  
        }
    }
}