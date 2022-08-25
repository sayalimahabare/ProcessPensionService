using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPensionDetails.Migrations
{
    public partial class pensiondetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PensionerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    PAN = table.Column<string>(nullable: true),
                    SalaryEarned = table.Column<double>(nullable: false),
                    Allowances = table.Column<int>(nullable: false),
                    SelfOrFamilyPension = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<double>(nullable: false),
                    PublicOrPrivate = table.Column<string>(nullable: true),
                    AdharNumber = table.Column<string>(nullable: true),
                    pensionAmount = table.Column<double>(nullable: false),
                    serviceCharge = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionerDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PensionerDetails");
        }
    }
}
