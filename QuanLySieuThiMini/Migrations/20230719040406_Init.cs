using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySieuThiMini.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    empID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    empAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.empID);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    guestPhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    guestName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.guestPhone);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    typeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    typeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.typeID);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    billID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalPrice = table.Column<int>(type: "int", nullable: false),
                    guestPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    guestPhone1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empID = table.Column<int>(type: "int", nullable: false),
                    employeeempID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.billID);
                    table.ForeignKey(
                        name: "FK_Bills_Employees_employeeempID",
                        column: x => x.employeeempID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Guests_guestPhone1",
                        column: x => x.guestPhone1,
                        principalTable: "Guests",
                        principalColumn: "guestPhone",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    proID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cost = table.Column<int>(type: "int", nullable: false),
                    inventory = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.proID);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_typeID",
                        column: x => x.typeID,
                        principalTable: "ProductTypes",
                        principalColumn: "typeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    billID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    proID = table.Column<int>(type: "int", nullable: false),
                    billName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => new { x.billID, x.proID });
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_billID",
                        column: x => x.billID,
                        principalTable: "Bills",
                        principalColumn: "billID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_proID",
                        column: x => x.proID,
                        principalTable: "Products",
                        principalColumn: "proID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_proID",
                table: "BillDetails",
                column: "proID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_employeeempID",
                table: "Bills",
                column: "employeeempID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_guestPhone1",
                table: "Bills",
                column: "guestPhone1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_typeID",
                table: "Products",
                column: "typeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
