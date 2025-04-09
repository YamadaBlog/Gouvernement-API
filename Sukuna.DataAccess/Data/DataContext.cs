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
        public DbSet<Moderateur> Moderateurs { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Statistique> Statistiques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation : Evenement -> Badge (optionnelle)
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Badge)
                        .WithMany(b => b.Evenements)
                        .HasForeignKey(e => e.IdBadge)
                        .OnDelete(DeleteBehavior.SetNull);

            // Nouvelle relation : Evenement -> Moderateur (many-to-one)
            // Un événement peut être validé par un modérateur (optionnel)
            // et un modérateur peut valider plusieurs événements.
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Moderateur)
                        .WithMany(m => m.EvenementsValides)
                        .HasForeignKey(e => e.IdModerateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Relation : Evenement -> Organisateur (Utilisateur)
            modelBuilder.Entity<Evenement>()
                        .HasOne(e => e.Organisateur)
                        .WithMany(u => u.EvenementsOrganises)
                        .HasForeignKey(e => e.IdOrganisateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Relation : Utilisateur -> Participations
            modelBuilder.Entity<Participation>()
                        .HasOne(p => p.Utilisateur)
                        .WithMany(u => u.Participations)
                        .HasForeignKey(p => p.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Evenement -> Participations
            modelBuilder.Entity<Participation>()
                        .HasOne(p => p.Evenement)
                        .WithMany(e => e.Participations)
                        .HasForeignKey(p => p.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Utilisateur -> Commentaires
            modelBuilder.Entity<Commentaire>()
                        .HasOne(c => c.Utilisateur)
                        .WithMany(u => u.Commentaires)
                        .HasForeignKey(c => c.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Evenement -> Commentaires
            modelBuilder.Entity<Commentaire>()
                        .HasOne(c => c.Evenement)
                        .WithMany(e => e.Commentaires)
                        .HasForeignKey(c => c.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Evenement -> Ressources
            modelBuilder.Entity<Ressource>()
                        .HasOne(r => r.Evenement)
                        .WithMany(e => e.Ressources)
                        .HasForeignKey(r => r.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Utilisateur (Createur) -> Ressources
            modelBuilder.Entity<Ressource>()
                        .HasOne(r => r.Createur)
                        .WithMany(u => u.RessourcesCreees)
                        .HasForeignKey(r => r.IdCreateur)
                        .OnDelete(DeleteBehavior.Restrict);

            // Relation : Interaction -> Utilisateur
            modelBuilder.Entity<Interaction>()
                        .HasOne(i => i.Utilisateur)
                        .WithMany(u => u.Interactions)
                        .HasForeignKey(i => i.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Interaction -> Badge
            modelBuilder.Entity<Interaction>()
                        .HasOne(i => i.Badge)
                        .WithMany(b => b.Interactions)
                        .HasForeignKey(i => i.IdBadge)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Statistique -> Utilisateur
            modelBuilder.Entity<Statistique>()
                        .HasOne(s => s.Utilisateur)
                        .WithMany(u => u.Statistiques)
                        .HasForeignKey(s => s.IdUtilisateur)
                        .OnDelete(DeleteBehavior.Cascade);

            // Relation : Statistique -> Evenement
            modelBuilder.Entity<Statistique>()
                        .HasOne(s => s.Evenement)
                        .WithMany(e => e.Statistiques)
                        .HasForeignKey(s => s.IdEvenement)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
