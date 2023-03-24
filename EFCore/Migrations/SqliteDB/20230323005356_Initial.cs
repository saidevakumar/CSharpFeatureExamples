using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations.SqliteDB
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    str_fld_FirstName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 250, nullable: true),
                    str_fld_LastName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_Email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_Phone = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_Address = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_City = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_State = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    str_fld_Zipcode = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Size = table.Column<int>(type: "INTEGER", nullable: true),
                    Variety = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    Status = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Perishable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpirationDays = table.Column<int>(type: "INTEGER", nullable: true),
                    Refrigerated = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SalesGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesGroup", x => x.Id);
                    table.UniqueConstraint("AK_SalesGroup_State_Type", x => new { x.State, x.Type });
                });

            migrationBuilder.CreateTable(
                name: "Salesperson",
                columns: table => new
                {
                    SalespersonID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    SalesGroupState = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false, defaultValue: "CA"),
                    SalesGroupType = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Zipcode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesperson", x => x.SalespersonID);
                    table.ForeignKey(
                        name: "FK_Salesperson_SalesGroup_SalesGroupState_SalesGroupType",
                        columns: x => new { x.SalesGroupState, x.SalesGroupType },
                        principalTable: "SalesGroup",
                        principalColumns: new[] { "State", "Type" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalDue = table.Column<decimal>(type: "money", nullable: true),
                    Status = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('none')"),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false),
                    SalespersonID = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastUpdate = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_Order_Salesperson",
                        column: x => x.SalespersonID,
                        principalTable: "Salesperson",
                        principalColumn: "SalespersonID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductID = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_OrderItem_Product1",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_str_fld_LastName",
                table: "Customer",
                column: "str_fld_LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Order",
                table: "Order",
                column: "OrderDate",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SalespersonID",
                table: "Order",
                column: "SalespersonID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductID",
                table: "OrderItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StateType",
                table: "SalesGroup",
                columns: new[] { "State", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salesperson_SalesGroupState_SalesGroupType",
                table: "Salesperson",
                columns: new[] { "SalesGroupState", "SalesGroupType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Salesperson");

            migrationBuilder.DropTable(
                name: "SalesGroup");
        }
    }
}
