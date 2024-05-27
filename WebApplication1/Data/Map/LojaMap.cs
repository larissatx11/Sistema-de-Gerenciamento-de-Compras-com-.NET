using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Data.Map;

public class LojaMap : IEntityTypeConfiguration<LojaModel>{

    public void Configure(EntityTypeBuilder<LojaModel> builder){
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Lojas)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui lojas quando um usuário é excluído
    }
}