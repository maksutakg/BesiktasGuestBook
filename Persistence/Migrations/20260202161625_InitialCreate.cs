using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mahalleler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ilce = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mahalleler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    HashPassword = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MahalleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_mahalleler_MahalleId",
                        column: x => x.MahalleId,
                        principalTable: "mahalleler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "mahalleler",
                columns: new[] { "Id", "Ilce", "Name" },
                values: new object[,]
                {
                    { 1, "Beşiktaş", "Abbasağa" },
                    { 2, "Beşiktaş", "Akat" },
                    { 3, "Beşiktaş", "Arnavutköy" },
                    { 4, "Beşiktaş", "Balmumcu" },
                    { 5, "Beşiktaş", "Bebek" },
                    { 6, "Beşiktaş", "Cihannüma" },
                    { 7, "Beşiktaş", "Dikilitaş" },
                    { 8, "Beşiktaş", "Etiler" },
                    { 9, "Beşiktaş", "Gayrettepe" },
                    { 10, "Beşiktaş", "Konaklar" },
                    { 11, "Beşiktaş", "Kuruçeşme" },
                    { 12, "Beşiktaş", "Kültür" },
                    { 13, "Beşiktaş", "Levazım" },
                    { 14, "Beşiktaş", "Levent" },
                    { 15, "Beşiktaş", "Mecidiye" },
                    { 16, "Beşiktaş", "Muradiye" },
                    { 17, "Beşiktaş", "Nispetiye" },
                    { 18, "Beşiktaş", "Ortaköy" },
                    { 19, "Beşiktaş", "Sinanpaşa" },
                    { 20, "Beşiktaş", "Türkali" },
                    { 21, "Beşiktaş", "Ulus" },
                    { 22, "Beşiktaş", "Vişnezade" },
                    { 23, "Beşiktaş", "Yıldız" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_MahalleId",
                table: "Notes",
                column: "MahalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mail",
                table: "Users",
                column: "Mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "mahalleler");
        }
    }
}
