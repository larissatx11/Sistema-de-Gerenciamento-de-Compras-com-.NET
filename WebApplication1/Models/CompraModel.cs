using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Models;

[Table("Compras")]
public class CompraModel{

    [Display(Name ="Código")]
    [Column("id")]
    public int Id{ get; set;}

    [Required(ErrorMessage = "Selecione um produto")]
    [Display(Name ="ProdutoId")]
    [Column("ProdutoId")]
    public int ProdutoId { get; set;}

    [Display(Name = "UsuarioId")]
    [Column("UsuarioId")]
    public int UsuarioId { get; set;}

    public ProdutosModel Produto { get; set;}

    public UsuarioModel Usuario { get; set;}
}