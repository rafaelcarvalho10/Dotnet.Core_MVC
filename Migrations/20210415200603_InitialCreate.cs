using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Correto.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Igreja",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_igreja = table.Column<string>(nullable: false),
                    nome_igreja = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igreja", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lancamentos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_lancamento = table.Column<string>(nullable: false),
                    Data_Lancamento = table.Column<DateTime>(nullable: false),
                    Qtd_pessoas = table.Column<float>(nullable: false),
                    Vl_oferta = table.Column<float>(nullable: false),
                    Qtd_dizimistas = table.Column<float>(nullable: false),
                    Vl_total_dizimos = table.Column<float>(nullable: false),
                    igrejaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lancamentos_Igreja_igrejaId",
                        column: x => x.igrejaId,
                        principalTable: "Igreja",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Igreja_Usuario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    igrejaId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igreja_Usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Igreja_Usuario_Igreja_igrejaId",
                        column: x => x.igrejaId,
                        principalTable: "Igreja",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Igreja_Usuario_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igreja_Usuario_igrejaId",
                table: "Igreja_Usuario",
                column: "igrejaId");

            migrationBuilder.CreateIndex(
                name: "IX_Igreja_Usuario_userId",
                table: "Igreja_Usuario",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_igrejaId",
                table: "Lancamentos",
                column: "igrejaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igreja_Usuario");

            migrationBuilder.DropTable(
                name: "Lancamentos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Igreja");
        }
    }
}
