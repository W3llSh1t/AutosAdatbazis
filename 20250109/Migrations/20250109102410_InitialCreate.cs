using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20250109.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rendszam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autok", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Adatok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdatID = table.Column<int>(type: "int", nullable: false),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evjarat = table.Column<int>(type: "int", nullable: false),
                    AutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adatok", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Adatok_Autok_AutoID",
                        column: x => x.AutoID,
                        principalTable: "Autok",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adatok_AutoID",
                table: "Adatok",
                column: "AutoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adatok");

            migrationBuilder.DropTable(
                name: "Autok");
        }
    }
}
