using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data.Map;

namespace WebApplication1.Data;

public class BancoContext : DbContext{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options){ 
        //construtor
    }

    public DbSet<LojaModel> Lojas { get; set; }
    public DbSet<ProdutosModel> Produtos { get; set; }
    public DbSet<UsuarioModel> Usuarios { get; set; }
    public DbSet<CompraModel> Compras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new LojaMap());
        modelBuilder.ApplyConfiguration(new ProdutoMap());
        modelBuilder.ApplyConfiguration(new CompraMap());
        base.OnModelCreating(modelBuilder);
    }
}
