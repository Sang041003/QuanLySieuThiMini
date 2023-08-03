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
                name: "Positions",
                columns: table => new
                {
                    posID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    posName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.posID);
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
                name: "Shelves",
                columns: table => new
                {
                    shelfID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shelfLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.shelfID);
                });

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
                    empPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    posID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.empID);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_posID",
                        column: x => x.posID,
                        principalTable: "Positions",
                        principalColumn: "posID",
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
                    shelfID = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Products_Shelves_shelfID",
                        column: x => x.shelfID,
                        principalTable: "Shelves",
                        principalColumn: "shelfID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    billID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalPrice = table.Column<int>(type: "int", nullable: false),
                    guestPhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.billID);
                    table.ForeignKey(
                        name: "FK_Bills_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Guests_guestPhone",
                        column: x => x.guestPhone,
                        principalTable: "Guests",
                        principalColumn: "guestPhone",
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
                name: "IX_Bills_empID",
                table: "Bills",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_guestPhone",
                table: "Bills",
                column: "guestPhone");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_posID",
                table: "Employees",
                column: "posID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_shelfID",
                table: "Products",
                column: "shelfID");

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

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
