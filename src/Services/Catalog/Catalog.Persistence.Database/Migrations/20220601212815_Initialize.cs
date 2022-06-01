using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KodotiCatalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "KodotiCatalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "Decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "KodotiCatalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "KodotiCatalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 001", "Product 001", 896m },
                    { 2, "Description for product 002", "Product 002", 950m },
                    { 3, "Description for product 003", "Product 003", 557m },
                    { 4, "Description for product 004", "Product 004", 252m },
                    { 5, "Description for product 005", "Product 005", 737m },
                    { 6, "Description for product 006", "Product 006", 237m },
                    { 7, "Description for product 007", "Product 007", 678m },
                    { 8, "Description for product 008", "Product 008", 381m },
                    { 9, "Description for product 009", "Product 009", 283m },
                    { 10, "Description for product 010", "Product 010", 976m },
                    { 11, "Description for product 011", "Product 011", 793m },
                    { 12, "Description for product 012", "Product 012", 543m },
                    { 13, "Description for product 013", "Product 013", 935m },
                    { 14, "Description for product 014", "Product 014", 494m },
                    { 15, "Description for product 015", "Product 015", 391m },
                    { 16, "Description for product 016", "Product 016", 289m },
                    { 17, "Description for product 017", "Product 017", 383m },
                    { 18, "Description for product 018", "Product 018", 583m },
                    { 19, "Description for product 019", "Product 019", 437m },
                    { 20, "Description for product 020", "Product 020", 796m },
                    { 21, "Description for product 021", "Product 021", 432m },
                    { 22, "Description for product 022", "Product 022", 833m },
                    { 23, "Description for product 023", "Product 023", 725m },
                    { 24, "Description for product 024", "Product 024", 158m },
                    { 25, "Description for product 025", "Product 025", 736m },
                    { 26, "Description for product 026", "Product 026", 224m },
                    { 27, "Description for product 027", "Product 027", 600m },
                    { 28, "Description for product 028", "Product 028", 580m },
                    { 29, "Description for product 029", "Product 029", 195m },
                    { 30, "Description for product 030", "Product 030", 503m },
                    { 31, "Description for product 031", "Product 031", 564m },
                    { 32, "Description for product 032", "Product 032", 396m },
                    { 33, "Description for product 033", "Product 033", 414m },
                    { 34, "Description for product 034", "Product 034", 988m },
                    { 35, "Description for product 035", "Product 035", 972m },
                    { 36, "Description for product 036", "Product 036", 609m },
                    { 37, "Description for product 037", "Product 037", 693m },
                    { 38, "Description for product 038", "Product 038", 641m },
                    { 39, "Description for product 039", "Product 039", 884m },
                    { 40, "Description for product 040", "Product 040", 593m },
                    { 41, "Description for product 041", "Product 041", 336m },
                    { 42, "Description for product 042", "Product 042", 630m }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 43, "Description for product 043", "Product 043", 682m },
                    { 44, "Description for product 044", "Product 044", 639m },
                    { 45, "Description for product 045", "Product 045", 294m },
                    { 46, "Description for product 046", "Product 046", 199m },
                    { 47, "Description for product 047", "Product 047", 309m },
                    { 48, "Description for product 048", "Product 048", 467m },
                    { 49, "Description for product 049", "Product 049", 841m },
                    { 50, "Description for product 050", "Product 050", 510m },
                    { 51, "Description for product 051", "Product 051", 723m },
                    { 52, "Description for product 052", "Product 052", 249m },
                    { 53, "Description for product 053", "Product 053", 298m },
                    { 54, "Description for product 054", "Product 054", 632m },
                    { 55, "Description for product 055", "Product 055", 763m },
                    { 56, "Description for product 056", "Product 056", 603m },
                    { 57, "Description for product 057", "Product 057", 954m },
                    { 58, "Description for product 058", "Product 058", 210m },
                    { 59, "Description for product 059", "Product 059", 966m },
                    { 60, "Description for product 060", "Product 060", 105m },
                    { 61, "Description for product 061", "Product 061", 617m },
                    { 62, "Description for product 062", "Product 062", 256m },
                    { 63, "Description for product 063", "Product 063", 486m },
                    { 64, "Description for product 064", "Product 064", 552m },
                    { 65, "Description for product 065", "Product 065", 671m },
                    { 66, "Description for product 066", "Product 066", 604m },
                    { 67, "Description for product 067", "Product 067", 490m },
                    { 68, "Description for product 068", "Product 068", 206m },
                    { 69, "Description for product 069", "Product 069", 896m },
                    { 70, "Description for product 070", "Product 070", 449m },
                    { 71, "Description for product 071", "Product 071", 376m },
                    { 72, "Description for product 072", "Product 072", 242m },
                    { 73, "Description for product 073", "Product 073", 535m },
                    { 74, "Description for product 074", "Product 074", 594m },
                    { 75, "Description for product 075", "Product 075", 508m },
                    { 76, "Description for product 076", "Product 076", 222m },
                    { 77, "Description for product 077", "Product 077", 426m },
                    { 78, "Description for product 078", "Product 078", 657m },
                    { 79, "Description for product 079", "Product 079", 954m },
                    { 80, "Description for product 080", "Product 080", 391m },
                    { 81, "Description for product 081", "Product 081", 245m },
                    { 82, "Description for product 082", "Product 082", 227m },
                    { 83, "Description for product 083", "Product 083", 233m },
                    { 84, "Description for product 084", "Product 084", 106m }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 85, "Description for product 085", "Product 085", 107m },
                    { 86, "Description for product 086", "Product 086", 243m },
                    { 87, "Description for product 087", "Product 087", 572m },
                    { 88, "Description for product 088", "Product 088", 658m },
                    { 89, "Description for product 089", "Product 089", 477m },
                    { 90, "Description for product 090", "Product 090", 192m },
                    { 91, "Description for product 091", "Product 091", 667m },
                    { 92, "Description for product 092", "Product 092", 444m },
                    { 93, "Description for product 093", "Product 093", 759m },
                    { 94, "Description for product 094", "Product 094", 863m },
                    { 95, "Description for product 095", "Product 095", 171m },
                    { 96, "Description for product 096", "Product 096", 277m },
                    { 97, "Description for product 097", "Product 097", 153m },
                    { 98, "Description for product 098", "Product 098", 168m },
                    { 99, "Description for product 099", "Product 099", 557m },
                    { 100, "Description for product 100", "Product 100", 798m }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 43 },
                    { 2, 2, 50 },
                    { 3, 3, 92 },
                    { 4, 4, 75 },
                    { 5, 5, 54 },
                    { 6, 6, 68 },
                    { 7, 7, 51 },
                    { 8, 8, 37 },
                    { 9, 9, 3 },
                    { 10, 10, 27 },
                    { 11, 11, 63 },
                    { 12, 12, 35 },
                    { 13, 13, 41 },
                    { 14, 14, 17 },
                    { 15, 15, 39 },
                    { 16, 16, 38 },
                    { 17, 17, 37 },
                    { 18, 18, 78 },
                    { 19, 19, 9 },
                    { 20, 20, 24 },
                    { 21, 21, 90 },
                    { 22, 22, 65 },
                    { 23, 23, 44 },
                    { 24, 24, 94 },
                    { 25, 25, 4 },
                    { 26, 26, 83 },
                    { 27, 27, 41 },
                    { 28, 28, 56 },
                    { 29, 29, 37 },
                    { 30, 30, 90 },
                    { 31, 31, 11 },
                    { 32, 32, 69 },
                    { 33, 33, 89 },
                    { 34, 34, 66 },
                    { 35, 35, 55 },
                    { 36, 36, 13 },
                    { 37, 37, 55 },
                    { 38, 38, 69 },
                    { 39, 39, 93 },
                    { 40, 40, 34 },
                    { 41, 41, 44 },
                    { 42, 42, 28 }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 43, 43, 54 },
                    { 44, 44, 80 },
                    { 45, 45, 40 },
                    { 46, 46, 70 },
                    { 47, 47, 69 },
                    { 48, 48, 61 },
                    { 49, 49, 93 },
                    { 50, 50, 62 },
                    { 51, 51, 99 },
                    { 52, 52, 25 },
                    { 53, 53, 82 },
                    { 54, 54, 77 },
                    { 55, 55, 96 },
                    { 56, 56, 15 },
                    { 57, 57, 91 },
                    { 58, 58, 11 },
                    { 59, 59, 69 },
                    { 60, 60, 23 },
                    { 61, 61, 11 },
                    { 62, 62, 97 },
                    { 63, 63, 13 },
                    { 64, 64, 44 },
                    { 65, 65, 54 },
                    { 66, 66, 12 },
                    { 67, 67, 26 },
                    { 68, 68, 15 },
                    { 69, 69, 57 },
                    { 70, 70, 39 },
                    { 71, 71, 31 },
                    { 72, 72, 91 },
                    { 73, 73, 80 },
                    { 74, 74, 80 },
                    { 75, 75, 10 },
                    { 76, 76, 81 },
                    { 77, 77, 40 },
                    { 78, 78, 42 },
                    { 79, 79, 97 },
                    { 80, 80, 12 },
                    { 81, 81, 11 },
                    { 82, 82, 8 },
                    { 83, 83, 45 },
                    { 84, 84, 53 }
                });

            migrationBuilder.InsertData(
                schema: "KodotiCatalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 85, 85, 20 },
                    { 86, 86, 91 },
                    { 87, 87, 91 },
                    { 88, 88, 89 },
                    { 89, 89, 2 },
                    { 90, 90, 38 },
                    { 91, 91, 52 },
                    { 92, 92, 67 },
                    { 93, 93, 53 },
                    { 94, 94, 47 },
                    { 95, 95, 97 },
                    { 96, 96, 67 },
                    { 97, 97, 50 },
                    { 98, 98, 69 },
                    { 99, 99, 36 },
                    { 100, 100, 86 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "KodotiCatalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "KodotiCatalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "KodotiCatalog");
        }
    }
}
