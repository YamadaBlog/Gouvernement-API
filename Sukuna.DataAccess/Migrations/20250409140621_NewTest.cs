using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukuna.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    IdBadge = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.IdBadge);
                });

            migrationBuilder.CreateTable(
                name: "Moderateurs",
                columns: table => new
                {
                    IdModerateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatutValidation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderateurs", x => x.IdModerateur);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.IdUtilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    IdEvenement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreParticipantsMin = table.Column<int>(type: "int", nullable: false),
                    NombreParticipantsMax = table.Column<int>(type: "int", nullable: false),
                    Accessibilite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOrganisateur = table.Column<int>(type: "int", nullable: false),
                    IdBadge = table.Column<int>(type: "int", nullable: true),
                    Etat = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdModerateur = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.IdEvenement);
                    table.ForeignKey(
                        name: "FK_Evenements_Badges_IdBadge",
                        column: x => x.IdBadge,
                        principalTable: "Badges",
                        principalColumn: "IdBadge",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Evenements_Moderateurs_IdModerateur",
                        column: x => x.IdModerateur,
                        principalTable: "Moderateurs",
                        principalColumn: "IdModerateur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evenements_Utilisateurs_IdOrganisateur",
                        column: x => x.IdOrganisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdBadge = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Badges_IdBadge",
                        column: x => x.IdBadge,
                        principalTable: "Badges",
                        principalColumn: "IdBadge",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interactions_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    IdCommentaire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdEvenement = table.Column<int>(type: "int", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.IdCommentaire);
                    table.ForeignKey(
                        name: "FK_Commentaires_Evenements_IdEvenement",
                        column: x => x.IdEvenement,
                        principalTable: "Evenements",
                        principalColumn: "IdEvenement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commentaires_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    IdParticipation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdEvenement = table.Column<int>(type: "int", nullable: false),
                    RoleParticipation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateParticipation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.IdParticipation);
                    table.ForeignKey(
                        name: "FK_Participations_Evenements_IdEvenement",
                        column: x => x.IdEvenement,
                        principalTable: "Evenements",
                        principalColumn: "IdEvenement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participations_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ressources",
                columns: table => new
                {
                    IdRessource = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCreateur = table.Column<int>(type: "int", nullable: false),
                    IdEvenement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressources", x => x.IdRessource);
                    table.ForeignKey(
                        name: "FK_Ressources_Evenements_IdEvenement",
                        column: x => x.IdEvenement,
                        principalTable: "Evenements",
                        principalColumn: "IdEvenement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ressources_Utilisateurs_IdCreateur",
                        column: x => x.IdCreateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statistiques",
                columns: table => new
                {
                    IdStat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdEvenement = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistiques", x => x.IdStat);
                    table.ForeignKey(
                        name: "FK_Statistiques_Evenements_IdEvenement",
                        column: x => x.IdEvenement,
                        principalTable: "Evenements",
                        principalColumn: "IdEvenement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistiques_Utilisateurs_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_IdEvenement",
                table: "Commentaires",
                column: "IdEvenement");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_IdUtilisateur",
                table: "Commentaires",
                column: "IdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_IdBadge",
                table: "Evenements",
                column: "IdBadge");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_IdModerateur",
                table: "Evenements",
                column: "IdModerateur");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_IdOrganisateur",
                table: "Evenements",
                column: "IdOrganisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_IdBadge",
                table: "Interactions",
                column: "IdBadge");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_IdUtilisateur",
                table: "Interactions",
                column: "IdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_IdEvenement",
                table: "Participations",
                column: "IdEvenement");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_IdUtilisateur",
                table: "Participations",
                column: "IdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Ressources_IdCreateur",
                table: "Ressources",
                column: "IdCreateur");

            migrationBuilder.CreateIndex(
                name: "IX_Ressources_IdEvenement",
                table: "Ressources",
                column: "IdEvenement");

            migrationBuilder.CreateIndex(
                name: "IX_Statistiques_IdEvenement",
                table: "Statistiques",
                column: "IdEvenement");

            migrationBuilder.CreateIndex(
                name: "IX_Statistiques_IdUtilisateur",
                table: "Statistiques",
                column: "IdUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaires");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Ressources");

            migrationBuilder.DropTable(
                name: "Statistiques");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "Moderateurs");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
