﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sukuna.DataAccess.Data;

#nullable disable

namespace Sukuna.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240406203710_createAll")]
    partial class createAll
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sukuna.Common.Models.Article", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prix")
                        .HasColumnType("int");

                    b.Property<int>("QuantiteEnStock")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("TvaTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.HasIndex("TvaTypeID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Sukuna.Common.Models.ClientOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatutCommande")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.ToTable("ClientOrders");
                });

            modelBuilder.Entity("Sukuna.Common.Models.OrderLine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ArticleID")
                        .HasColumnType("int");

                    b.Property<int?>("ClientOrderID")
                        .HasColumnType("int");

                    b.Property<int>("PrixUnitaire")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<int?>("SupplierOrderID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ArticleID");

                    b.HasIndex("ClientOrderID");

                    b.HasIndex("SupplierOrderID");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Sukuna.Common.Models.SupplierOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatutCommande")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.HasIndex("UserID");

                    b.ToTable("SupplierOrders");
                });

            modelBuilder.Entity("Sukuna.Common.Models.TvaType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Taux")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TvaTypes");
                });

            modelBuilder.Entity("Sukuna.Common.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasseHashe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Article", b =>
                {
                    b.HasOne("Sukuna.Common.Models.Supplier", "Supplier")
                        .WithMany("Articles")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sukuna.Common.Models.TvaType", "TvaType")
                        .WithMany("Articles")
                        .HasForeignKey("TvaTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("TvaType");
                });

            modelBuilder.Entity("Sukuna.Common.Models.ClientOrder", b =>
                {
                    b.HasOne("Sukuna.Common.Models.Client", "Client")
                        .WithMany("ClientOrders")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Sukuna.Common.Models.OrderLine", b =>
                {
                    b.HasOne("Sukuna.Common.Models.Article", "Article")
                        .WithMany("OrderLines")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sukuna.Common.Models.ClientOrder", "ClientOrder")
                        .WithMany("OrderLines")
                        .HasForeignKey("ClientOrderID");

                    b.HasOne("Sukuna.Common.Models.SupplierOrder", "SupplierOrder")
                        .WithMany("OrderLines")
                        .HasForeignKey("SupplierOrderID");

                    b.Navigation("Article");

                    b.Navigation("ClientOrder");

                    b.Navigation("SupplierOrder");
                });

            modelBuilder.Entity("Sukuna.Common.Models.SupplierOrder", b =>
                {
                    b.HasOne("Sukuna.Common.Models.Supplier", "Supplier")
                        .WithMany("SupplierOrders")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sukuna.Common.Models.User", "User")
                        .WithMany("SupplierOrders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Article", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Client", b =>
                {
                    b.Navigation("ClientOrders");
                });

            modelBuilder.Entity("Sukuna.Common.Models.ClientOrder", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Sukuna.Common.Models.Supplier", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("SupplierOrders");
                });

            modelBuilder.Entity("Sukuna.Common.Models.SupplierOrder", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Sukuna.Common.Models.TvaType", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Sukuna.Common.Models.User", b =>
                {
                    b.Navigation("SupplierOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
