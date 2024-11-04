using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    Villa_Number = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.Villa_Number);
                    table.ForeignKey(
                        name: "FK_VillaNumbers_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "Villa_Number", "SpecialDetails", "VillaId" },
                values: new object[,]
                {
                    { 101, null, 1 },
                    { 102, null, 1 },
                    { 103, null, 1 },
                    { 104, null, 1 },
                    { 201, null, 2 },
                    { 202, null, 2 },
                    { 203, null, 2 },
                    { 204, null, 2 },
                    { 301, null, 3 },
                    { 302, null, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "The royal villa features towering marble columns, gilded ceilings, and expansive windows that open to manicured gardens, seamlessly blending historical grandeur with luxurious comfort.");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "The premium villa centers around a stunning infinity pool, with sleek modern architecture, open-air lounge spaces, and floor-to-ceiling glass walls that blend indoor luxury with breathtaking outdoor views.");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "The luxury pool villa offers an oasis of elegance, with a private pool surrounded by lush greenery, spacious sun decks, and sophisticated interiors that blend seamless indoor-outdoor living with refined comfort.");

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.");
        }
    }
}
