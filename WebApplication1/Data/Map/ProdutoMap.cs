using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Data.Map;

public class ProdutoMap : IEntityTypeConfiguration<ProdutosModel>{

    public void Configure(EntityTypeBuilder<ProdutosModel> builder){
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Loja)
                .WithMany(l => l.Produtos)
                .HasForeignKey(x => x.LojaId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui produtos quando uma loja é excluída

        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Produtos)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); // Exclui produtos quando um usuário é excluído
    }
}