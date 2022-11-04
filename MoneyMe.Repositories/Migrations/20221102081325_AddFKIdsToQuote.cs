using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    public partial class AddFKIdsToQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Repayment",
                table: "Quotes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repayment",
                table: "Quotes");
        }
    }
}
