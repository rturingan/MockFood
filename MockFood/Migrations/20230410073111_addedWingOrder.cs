using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockFood.Migrations
{
    public partial class addedWingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WingAssort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<float>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WingOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WingOrders");
        }
    }
}
