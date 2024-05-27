using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class UsuarioModel{
    public int Id { get; set;}

    [Required(ErrorMessage = "Digite o nome do usuário")]
    public required string Nome { get; set;}

    [Required(ErrorMessage = "Digite o e-mail do usuário")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
    public required string Email { get; set;}
    
    [Required(ErrorMessage = "Digite a senha do usuário")]
    public required string Senha { get; set;}

    public bool SenhaValida(string senha){
        return Senha == senha;
    }

    // Inicialização das listas
    public UsuarioModel()
    {
        Lojas = new List<LojaModel>();
        Produtos = new List<ProdutosModel>();
        Compras = new List<CompraModel>();
    }
    
    public virtual List<LojaModel> Lojas { get; set; }
    
    public virtual List<ProdutosModel> Produtos { get; set; }

    public virtual List<CompraModel> Compras { get; set; }
}