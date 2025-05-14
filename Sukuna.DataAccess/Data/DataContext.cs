using Microsoft.EntityFrameworkCore;
using Sukuna.Common.Models;

namespace Sukuna.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSet pour chacune des entités
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Statistique> Statistiques { get; set; }
        // (On a retiré DbSet<Moderateur> car ce n’est plus une table à part.)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evenement -> Badge (optionnelle)
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Badge)
                        .WithMany(b => b.Evenements)
                        .HasForeignKey(e => e.IdBadge)
                        .OnDelete(DeleteBehavior.SetNull);

            // Evenement -> Moderateur (Utilisateur) many-to-one
            // Un événement peut être validé par un utilisateur (modérateur),
            // et un utilisateur peut valider plusieurs événements.
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Moderateur)
                        .WithMany(u => u.EvenementsValides)
                        .HasForeignKey(e => e.IdModerateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Evenement -> Organisateur (Utilisateur)
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Organisateur)
                        .WithMany(u => u.EvenementsOrganises)
                        .HasForeignKey(e => e.IdOrganisateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Participations
            modelBuilder.Entity<Participation>()
                        .HasOne(p => p.Utilisateur)
                        .WithMany(u => u.Participations)
                        .HasForeignKey(p => p.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participation>()
                        .HasOne(p => p.Evenement)
                        .WithMany(e => e.Participations)
                        .HasForeignKey(p => p.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            // Commentaires
            modelBuilder.Entity<Commentaire>()
                        .HasOne(c => c.Utilisateur)
                        .WithMany(u => u.Commentaires)
                        .HasForeignKey(c => c.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Commentaire>()
                        .HasOne(c => c.Evenement)
                        .WithMany(e => e.Commentaires)
                        .HasForeignKey(c => c.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            // Ressources
            modelBuilder.Entity<Ressource>()
                        .HasOne(r => r.Evenement)
                        .WithMany(e => e.Ressources)
                        .HasForeignKey(r => r.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ressource>()
                        .HasOne(r => r.Createur)
                        .WithMany(u => u.RessourcesCreees)
                        .HasForeignKey(r => r.IdCreateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Interactions
            modelBuilder.Entity<Interaction>()
                        .HasOne(i => i.Utilisateur)
                        .WithMany(u => u.Interactions)
                        .HasForeignKey(i => i.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Interaction>()
                        .HasOne(i => i.Badge)
                        .WithMany(b => b.Interactions)
                        .HasForeignKey(i => i.IdBadge)
                        .OnDelete(DeleteBehavior.Cascade);

            // Statistiques
            modelBuilder.Entity<Statistique>()
                        .HasOne(s => s.Utilisateur)
                        .WithMany(u => u.Statistiques)
                        .HasForeignKey(s => s.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Statistique>()
                        .HasOne(s => s.Evenement)
                        .WithMany(e => e.Statistiques)
                        .HasForeignKey(s => s.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
