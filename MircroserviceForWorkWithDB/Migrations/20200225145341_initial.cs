using Microsoft.EntityFrameworkCore.Migrations;

namespace MircroserviceForWorkWithDB.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Temp = table.Column<float>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Pressure = table.Column<int>(nullable: false),
                    Wind = table.Column<float>(nullable: false),
                    Weather = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
