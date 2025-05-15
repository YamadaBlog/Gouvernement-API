using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sukuna.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Moderateurs_IdModerateur",
                table: "Evenements");

            migrationBuilder.DropTable(
                name: "Moderateurs");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Utilisateurs_IdModerateur",
                table: "Evenements",
                column: "IdModerateur",
                principalTable: "Utilisateurs",
                principalColumn: "IdUtilisateur",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Utilisateurs_IdModerateur",
                table: "Evenements");

            migrationBuilder.CreateTable(
                name: "Moderateurs",
                columns: table => new
                {
                    IdModerateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatutValidation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderateurs", x => x.IdModerateur);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Moderateurs_IdModerateur",
                table: "Evenements",
                column: "IdModerateur",
                principalTable: "Moderateurs",
                principalColumn: "IdModerateur",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
