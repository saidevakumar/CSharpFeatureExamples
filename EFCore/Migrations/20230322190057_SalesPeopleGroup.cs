using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SalesPeopleGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_SalesGroup_State_Type",
                table: "SalesGroup",
                columns: new[] { "State", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Salesperson_SalesGroupState_SalesGroupType",
                table: "Salesperson",
                columns: new[] { "SalesGroupState", "SalesGroupType" });

            migrationBuilder.AddForeignKey(
                name: "FK_Salesperson_SalesGroup_SalesGroupState_SalesGroupType",
                table: "Salesperson",
                columns: new[] { "SalesGroupState", "SalesGroupType" },
                principalTable: "SalesGroup",
                principalColumns: new[] { "State", "Type" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salesperson_SalesGroup_SalesGroupState_SalesGroupType",
                table: "Salesperson");

            migrationBuilder.DropIndex(
                name: "IX_Salesperson_SalesGroupState_SalesGroupType",
                table: "Salesperson");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SalesGroup_State_Type",
                table: "SalesGroup");
        }
    }
}
