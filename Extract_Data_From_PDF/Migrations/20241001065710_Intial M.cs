using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Extract_Data_From_PDF.Migrations
{
    /// <inheritdoc />
    public partial class IntialM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentRequested = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizationDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PdfDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Field2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItRequests");

            migrationBuilder.DropTable(
                name: "PdfDatas");
        }
    }
}
