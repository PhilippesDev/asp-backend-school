using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api_gestion_ecole.Migrations
{
    /// <inheritdoc />
    public partial class Ajout_Table_Niveau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NiveauId",
                table: "Classe",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Niveau",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classe_NiveauId",
                table: "Classe",
                column: "NiveauId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classe_Niveau_NiveauId",
                table: "Classe",
                column: "NiveauId",
                principalTable: "Niveau",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classe_Niveau_NiveauId",
                table: "Classe");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropIndex(
                name: "IX_Classe_NiveauId",
                table: "Classe");

            migrationBuilder.DropColumn(
                name: "NiveauId",
                table: "Classe");
        }
    }
}
