using Microsoft.EntityFrameworkCore.Migrations;

public class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                UserName = table.Column<string>(nullable: false),
                Email = table.Column<string>(nullable: false),
                Password = table.Column<string>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Role = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Cities",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                Name = table.Column<string>(maxLength: 255, nullable: false),
                XCoordinate = table.Column<int>(nullable: false),
                YCoordinate = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PassengerPlans",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                PassengerUserId = table.Column<int>(nullable: false),
                TravelPlanId = table.Column<int>(nullable: false),
                PassengerAndFriendsCount = table.Column<int>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Status = table.Column<string>(nullable: false),
                PassengerNote = table.Column<string>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PassengerPlans", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TravelPlans",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                DriverUserId = table.Column<int>(nullable: false),
                DepartureCityId = table.Column<int>(nullable: false),
                DestinationCityId = table.Column<int>(nullable: false),
                Price = table.Column<decimal>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                DepartureDate = table.Column<DateTime>(nullable: false),
                PassengerCount = table.Column<int>(nullable: false),
                TravelNote = table.Column<string>(nullable: true),
                Status = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TravelPlans", x => x.Id);
            });

        // Add cities using a for loop
        for (int x = 0; x < 1000; x += 50)
        {
            for (int y = 0; y < 500; y += 50)
            {
                migrationBuilder.InsertData(
                    table: "Cities",
                    columns: new[] { "Name", "XCoordinate", "YCoordinate" },
                    values: new object[] { $"City{x}-{y}", x, y });
            }
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Cities");

        migrationBuilder.DropTable(
            name: "PassengerPlans");

        migrationBuilder.DropTable(
            name: "TravelPlans");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
