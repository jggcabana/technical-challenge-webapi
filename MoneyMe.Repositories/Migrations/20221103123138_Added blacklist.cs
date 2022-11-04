using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    public partial class Addedblacklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blacklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlacklistType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlacklistValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blacklist", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blacklist",
                columns: new[] { "Id", "BlacklistType", "BlacklistValue", "IsActive" },
                values: new object[,]
                {
                    { 1, "EmailDomain", "twitter.com", true },
                    { 2, "EmailDomain", "tesla.com", true },
                    { 3, "MobileNumber", "123456789", true },
                    { 4, "MobileNumber", "987654321", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blacklist");
        }
    }
}
