using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models;

[Table("Lojas")]
public class LojaModel{

    [Display(Name ="Código")]
    [Column("id")]
    public int Id{ get; set;}

    [Required(ErrorMessage = "Digite o nome da loja")]
    [Display(Name ="Nome")]
    [Column("Nome")]
    public required string Nome { get; set;}

    [Required(ErrorMessage = "Digite o endereço da loja")]
    [Display(Name ="Endereco")]
    [Column("Endereco")]
    public required string Endereco { get; set;}

    [Display(Name = "UsuarioId")]
    [Column("UsuarioId")]
    public int UsuarioId { get; set;}

    public UsuarioModel Usuario { get; set;}

    public virtual ICollection<ProdutosModel> Produtos { get; set; }

}