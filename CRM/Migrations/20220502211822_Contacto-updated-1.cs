using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class Contactoupdated1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Prospectos_ProspectoId",
                table: "Contactos");

            migrationBuilder.AlterColumn<int>(
                name: "ProspectoId",
                table: "Contactos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Prospectos_ProspectoId",
                table: "Contactos",
                column: "ProspectoId",
                principalTable: "Prospectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Prospectos_ProspectoId",
                table: "Contactos");

            migrationBuilder.AlterColumn<int>(
                name: "ProspectoId",
                table: "Contactos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Prospectos_ProspectoId",
                table: "Contactos",
                column: "ProspectoId",
                principalTable: "Prospectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
