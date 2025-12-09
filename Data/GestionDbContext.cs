using Microsoft.EntityFrameworkCore;
using gestionCompte.Models;

namespace gestionCompte.Data
{
    public class GestionCompteContext : DbContext
    {
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Titulaire> Titulaires { get; set; }
        public DbSet<Statistiques> Statistiques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compte>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Compte>()
                .Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Compte>()
                .HasOne(c => c.Titulaire)
                .WithMany() 
                .HasForeignKey(c => c.TitulaireId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Compte>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Compte)
                .HasForeignKey(t => t.CompteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Montant)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Transaction>()
                .Property(t => t.SoldeApres)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Titulaire>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Titulaire>()
                .Property(t => t.Nom)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Statistiques>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Statistiques>()
                .Property(s => s.TotalDepots)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Statistiques>()
                .Property(s => s.TotalRetraits)
                .HasColumnType("decimal(18,2)");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=gestionCompteCS;Username=postgres;Password=Mouhamed-1234");
        }
    }
}
