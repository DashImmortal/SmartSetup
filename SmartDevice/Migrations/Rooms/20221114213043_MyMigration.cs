using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDevice.Migrations.Rooms
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartDeviceModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOn = table.Column<bool>(type: "bit", nullable: false),
                    RoomModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartDeviceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartDeviceModel_Rooms_RoomModelId",
                        column: x => x.RoomModelId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartDeviceModel_RoomModelId",
                table: "SmartDeviceModel",
                column: "RoomModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartDeviceModel");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
