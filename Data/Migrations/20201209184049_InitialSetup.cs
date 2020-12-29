using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Kursinis.Models;
using Kursinis.Data;

namespace Kursinis.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "Products",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy",
                                         SqlServerValueGenerationStrategy.IdentityColumn),
                        ItemName = table.Column<string>(nullable: true),
                        ItemDescription = table.Column<string>(nullable: true),
                        Price = table.Column<decimal>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Product", x => x.Id);
                    });
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "Products");
            }
        
    }
}
