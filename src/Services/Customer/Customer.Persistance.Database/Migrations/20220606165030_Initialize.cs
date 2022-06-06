using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Persistance.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KodotiCustomer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "KodotiCustomer",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "KodotiCustomer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Client 001" },
                    { 2, "Client 002" },
                    { 3, "Client 003" },
                    { 4, "Client 004" },
                    { 5, "Client 005" },
                    { 6, "Client 006" },
                    { 7, "Client 007" },
                    { 8, "Client 008" },
                    { 9, "Client 009" },
                    { 10, "Client 010" },
                    { 11, "Client 011" },
                    { 12, "Client 012" },
                    { 13, "Client 013" },
                    { 14, "Client 014" },
                    { 15, "Client 015" },
                    { 16, "Client 016" },
                    { 17, "Client 017" },
                    { 18, "Client 018" },
                    { 19, "Client 019" },
                    { 20, "Client 020" },
                    { 21, "Client 021" },
                    { 22, "Client 022" },
                    { 23, "Client 023" },
                    { 24, "Client 024" },
                    { 25, "Client 025" },
                    { 26, "Client 026" },
                    { 27, "Client 027" },
                    { 28, "Client 028" },
                    { 29, "Client 029" },
                    { 30, "Client 030" },
                    { 31, "Client 031" },
                    { 32, "Client 032" },
                    { 33, "Client 033" },
                    { 34, "Client 034" },
                    { 35, "Client 035" },
                    { 36, "Client 036" },
                    { 37, "Client 037" },
                    { 38, "Client 038" },
                    { 39, "Client 039" },
                    { 40, "Client 040" },
                    { 41, "Client 041" },
                    { 42, "Client 042" }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCustomer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 43, "Client 043" },
                    { 44, "Client 044" },
                    { 45, "Client 045" },
                    { 46, "Client 046" },
                    { 47, "Client 047" },
                    { 48, "Client 048" },
                    { 49, "Client 049" },
                    { 50, "Client 050" },
                    { 51, "Client 051" },
                    { 52, "Client 052" },
                    { 53, "Client 053" },
                    { 54, "Client 054" },
                    { 55, "Client 055" },
                    { 56, "Client 056" },
                    { 57, "Client 057" },
                    { 58, "Client 058" },
                    { 59, "Client 059" },
                    { 60, "Client 060" },
                    { 61, "Client 061" },
                    { 62, "Client 062" },
                    { 63, "Client 063" },
                    { 64, "Client 064" },
                    { 65, "Client 065" },
                    { 66, "Client 066" },
                    { 67, "Client 067" },
                    { 68, "Client 068" },
                    { 69, "Client 069" },
                    { 70, "Client 070" },
                    { 71, "Client 071" },
                    { 72, "Client 072" },
                    { 73, "Client 073" },
                    { 74, "Client 074" },
                    { 75, "Client 075" },
                    { 76, "Client 076" },
                    { 77, "Client 077" },
                    { 78, "Client 078" },
                    { 79, "Client 079" },
                    { 80, "Client 080" },
                    { 81, "Client 081" },
                    { 82, "Client 082" },
                    { 83, "Client 083" },
                    { 84, "Client 084" }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCustomer",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 85, "Client 085" },
                    { 86, "Client 086" },
                    { 87, "Client 087" },
                    { 88, "Client 088" },
                    { 89, "Client 089" },
                    { 90, "Client 090" },
                    { 91, "Client 091" },
                    { 92, "Client 092" },
                    { 93, "Client 093" },
                    { 94, "Client 094" },
                    { 95, "Client 095" },
                    { 96, "Client 096" },
                    { 97, "Client 097" },
                    { 98, "Client 098" },
                    { 99, "Client 099" },
                    { 100, "Client 100" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "KodotiCustomer");
        }
    }
}
