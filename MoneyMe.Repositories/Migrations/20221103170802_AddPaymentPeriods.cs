using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    public partial class AddPaymentPeriods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentPeriods",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentPeriods",
                table: "Quotes");
        }
    }
}
