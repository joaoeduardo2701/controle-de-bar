using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeBar.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoTBPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Produto_Id = table.Column<int>(type: "int", nullable: false),
                    QuantidadeSolicitada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBPedido_TBProduto_Produto_Id",
                        column: x => x.Produto_Id,
                        principalTable: "TBProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBPedido_Produto_Id",
                table: "TBPedido",
                column: "Produto_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBPedido");
        }
    }
}
