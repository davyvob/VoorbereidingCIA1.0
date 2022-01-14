using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Howest.Prog.CoinChop.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Token = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_ExpenseGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ExpenseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ContributorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Members_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseGroups",
                columns: new[] { "Id", "Name", "Token" },
                values: new object[] { 1L, "Friends funpark", "friendsfunpark" });

            migrationBuilder.InsertData(
                table: "ExpenseGroups",
                columns: new[] { "Id", "Name", "Token" },
                values: new object[] { 2L, "Raiding supplies", "privateers" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "GroupId", "Name" },
                values: new object[,]
                {
                    { 1L, "rachel@friends.example.com", 1L, "Rachel" },
                    { 2L, "monica@friends.example.com", 1L, "Monica" },
                    { 3L, "chandler@friends.example.com", 1L, "Chandler" },
                    { 4L, "joey@friends.example.com", 1L, "Joey" },
                    { 5L, "ross@friends.example.com", 1L, "Ross" },
                    { 6L, "jan@kapers.example.com", 2L, "Jan" },
                    { 7L, "pieredebeeste@kapers.example.com", 2L, "Pier" },
                    { 8L, "tjoris@kapers.example.com", 2L, "Tjoris" },
                    { 9L, "corneel@kapers.example.com", 2L, "Corneel" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "ContributorId", "Description" },
                values: new object[,]
                {
                    { 3L, 20.0m, 1L, "Lunch" },
                    { 1L, 20.0m, 2L, "Tickets mom and dad" },
                    { 5L, 10.0m, 3L, "Waffles and coffee" },
                    { 6L, 5.0m, 4L, "Souvenir photo" },
                    { 2L, 30.0m, 5L, "Tickets for myself, bro and sis" },
                    { 4L, 10.0m, 5L, "Ice cream" },
                    { 7L, 200.0m, 7L, "Cannonballs" },
                    { 10L, 60.0m, 7L, "Gunpowder" },
                    { 8L, 40.0m, 8L, "Mizzen-mast rigging ropes" },
                    { 9L, 75.0m, 9L, "Heavy loot chest" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ContributorId",
                table: "Expenses",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "ExpenseGroups");
        }
    }
}
