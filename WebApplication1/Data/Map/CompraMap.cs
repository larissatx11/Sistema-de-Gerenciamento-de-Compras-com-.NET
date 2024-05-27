using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Data.Map;

public class CompraMap : IEntityTypeConfiguration<CompraModel>{

    public void Configure(EntityTypeBuilder<CompraModel> builder){
        builder.HasKey(x => x.Id);
       // Relacionamento com o usuário
       // Relacionamento com o usuário
            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Compras)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui compras quando um usuário é excluído

            // Relacionamento com os produtos da compra
            builder.HasOne(c => c.Produto)
                .WithMany(p => p.Compras)
                .HasForeignKey(c => c.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui compras quando um produto é excluído
    }
}