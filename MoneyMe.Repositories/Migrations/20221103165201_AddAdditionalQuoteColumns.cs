using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    public partial class AddAdditionalQuoteColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blacklist",
                table: "Blacklist");

            migrationBuilder.RenameTable(
                name: "Blacklist",
                newName: "Blacklists");

            migrationBuilder.AddColumn<decimal>(
                name: "EstablishmentFee",
                table: "Quotes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestAmount",
                table: "Quotes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blacklists",
                table: "Blacklists",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blacklists",
                table: "Blacklists");

            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "InterestAmount",
                table: "Quotes");

            migrationBuilder.RenameTable(
                name: "Blacklists",
                newName: "Blacklist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blacklist",
                table: "Blacklist",
                column: "Id");
        }
    }
}
