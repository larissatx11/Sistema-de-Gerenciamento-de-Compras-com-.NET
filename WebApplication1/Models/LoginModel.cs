using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class LoginModel{
    [Required(ErrorMessage = "Digite o e-mail")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
    public required string Email { get; set;}
    
    [Required(ErrorMessage = "Digite a senha")]
    public required string Senha { get; set;}
}
