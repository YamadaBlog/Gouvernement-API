using Microsoft.EntityFrameworkCore;
using Sukuna.Common.Models;

namespace Sukuna.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Utilisateur> Articles { get; set; }
        public DbSet<Evenement> Clients { get; set; }
        public DbSet<Interaction> Users { get; set; }
        public DbSet<Commentaire> Suppliers { get; set; }
        public DbSet<Participation> ClientOrders { get; set; }
        public DbSet<Moderateur> OrderLines { get; set; }
        public DbSet<Ressource> SupplierOrders { get; set; }
        public DbSet<Badge> TvaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer la relation entre Ressource et Moderateur
            modelBuilder.Entity<Utilisateur>()
                .HasOne(p => p.TvaType)
                .WithMany(pc => pc.Articles)
                .HasForeignKey(p => p.TvaTypeID);
            modelBuilder.Entity<Utilisateur>()
                .HasOne(p => p.Supplier)
                .WithMany(pc => pc.Articles)
                .HasForeignKey(p => p.SupplierID);

            // Configurer la relation entre Ressource et Moderateur
            modelBuilder.Entity<Moderateur>()
                .HasOne(c => c.ClientOrder)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.ClientOrderID);
            modelBuilder.Entity<Moderateur>()
                .HasOne(c => c.SupplierOrder)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.SupplierOrderID);
            modelBuilder.Entity<Moderateur>()
                .HasOne(c => c.Article)
                .WithMany(pc => pc.OrderLines)
                .HasForeignKey(p => p.ArticleID);

            // Configurer la relation entre Ressource et Moderateur
            modelBuilder.Entity<Ressource>()
                .HasOne(p => p.User)
                .WithMany(pc => pc.SupplierOrders)
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<Ressource>()
           .HasOne(p => p.Supplier)
           .WithMany(pc => pc.SupplierOrders)
           .HasForeignKey(c => c.SupplierID);

            // Configurer la relation entre Ressource et Moderateur
            modelBuilder.Entity<Participation>()
                .HasOne(p => p.Client)
                .WithMany(pc => pc.ClientOrders)
                .HasForeignKey(p => p.ClientID);

        }
    }
}