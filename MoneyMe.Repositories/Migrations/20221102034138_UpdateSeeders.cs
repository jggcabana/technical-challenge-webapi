using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    public partial class UpdateSeeders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StartFromNMonth",
                table: "Interests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Description", "DurationMax", "DurationMin", "Name", "Rate", "StartFromNMonth" },
                values: new object[] { 1, "Standard Interest", -1, 0, "Standard", 0.050000000000000003, 1 });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Description", "DurationMax", "DurationMin", "Name", "Rate", "StartFromNMonth" },
                values: new object[] { 2, "First 2 Months Interest Free", -1, 6, "First 2 Months Free", 0.050000000000000003, 3 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "InterestId", "Name" },
                values: new object[] { 1, "Interest Free Loan", null, "ProductA" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "InterestId", "Name" },
                values: new object[] { 2, "First 2 Months Interest Free", 2, "ProductB" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "InterestId", "Name" },
                values: new object[] { 3, "Standard Interest", 1, "ProductC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "StartFromNMonth",
                table: "Interests");
        }
    }
}
