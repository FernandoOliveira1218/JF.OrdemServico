using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JF.OrdemServico.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Clientes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Clientes",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Chamados",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SenhaHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientesUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesUsuarios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Cnpj", "DataAlteracao", "DataCriacao", "Email", "Nome", "RazaoSocial" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "12345678000100", null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "clientepadrao@cliente.com", "Cliente Padrão", "Cliente Padrão LTDA" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Email", "Login", "Nome", "SenhaHash" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "admin@admin.com", "admin", "Administrador", "$2a$11$4LC85MBAVfGLbtymgz8VcetHq7NMg/wUQppvHomk6whsCJXG3.ony" });

            migrationBuilder.InsertData(
                table: "ClientesUsuarios",
                columns: new[] { "Id", "ClienteId", "DataAlteracao", "DataCriacao", "UsuarioId" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesUsuarios_ClienteId",
                table: "ClientesUsuarios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesUsuarios_UsuarioId",
                table: "ClientesUsuarios",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesUsuarios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Chamados");
        }
    }
}
