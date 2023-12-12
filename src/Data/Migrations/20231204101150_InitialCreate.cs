using Microsoft.EntityFrameworkCore.Migrations;


namespace Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Email = table.Column<string>(type: "TEXT", nullable: false),
                PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                SecondName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                ThirdName = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                Email = table.Column<string>(type: "TEXT", nullable: false),
                Address = table.Column<string>(type: "TEXT", nullable: false),
                Factory = table.Column<string>(type: "TEXT", nullable: false),
                DateAdded = table.Column<DateOnly>(type: "TEXT", nullable: false),
                Status = table.Column<int>(type: "INTEGER", nullable: false),
                UserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
                table.ForeignKey(
                    name: "FK_Clients_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                Price = table.Column<int>(type: "INTEGER", nullable: false),
                UserId = table.Column<int>(type: "INTEGER", nullable: false),
                ClientId = table.Column<int>(type: "INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Products_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reminder",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Note = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: true),
                When = table.Column<DateTime>(type: "TEXT", nullable: false),
                ClientId = table.Column<int>(type: "INTEGER", nullable: true),
                UserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reminder", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reminder_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Reminder_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Characteristic",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ProductType = table.Column<string>(type: "TEXT", nullable: false),
                Category = table.Column<string>(type: "TEXT", nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                Country = table.Column<string>(type: "TEXT", nullable: false),
                ManufactureDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                Status = table.Column<int>(type: "INTEGER", nullable: false),
                ProductId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Characteristic", x => x.Id);
                table.ForeignKey(
                    name: "FK_Characteristic_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Characteristic_ProductId",
            table: "Characteristic",
            column: "ProductId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Clients_UserId",
            table: "Clients",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_ClientId",
            table: "Products",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_UserId",
            table: "Products",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Reminder_ClientId",
            table: "Reminder",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Reminder_UserId",
            table: "Reminder",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Characteristic");

        migrationBuilder.DropTable(
            name: "Reminder");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Clients");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
