using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyFromProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IF EXISTS `IX_Produtos_LojaId` ON `Produtos`");
            migrationBuilder.CreateIndex(
                name: "IX_Produtos_LojaId",
                table: "Produtos",
                column: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Lojas_LojaId",
                table: "Produtos",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Lojas_LojaId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_LojaId",
                table: "Produtos");
        }
    }
}
