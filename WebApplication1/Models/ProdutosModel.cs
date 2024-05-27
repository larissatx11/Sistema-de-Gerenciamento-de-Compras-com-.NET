using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models;

[Table("Produtos")]
public class ProdutosModel{

    [Display(Name ="Código")]
    [Column("id")]
    public int Id{ get; set;}

    [Required(ErrorMessage = "Digite o nome do produto")]
    [Display(Name ="Nome")]
    [Column("Nome")]
    public required string Nome { get; set;}

    [Required(ErrorMessage = "Digite o preço do produto")]
    [Display(Name ="Preco")]
    [Column("Preco")]
    public double Preco{ get; set;}

    [Required(ErrorMessage = "Selecione a loja do produto")]
    [Display(Name = "LojaId")]
    [Column("LojaId")]
    public int LojaId { get; set;}

    public LojaModel Loja { get; set;}

    [Display(Name = "UsuarioId")]
    [Column("UsuarioId")]
    public int? UsuarioId { get; set;}

    public UsuarioModel Usuario { get; set;}

    public virtual ICollection<CompraModel> Compras { get; set; }
}