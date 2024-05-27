using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositorio;
using WebApplication1.Helper;

namespace WebApplication1.Controllers;

public class LoginController : Controller{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;
    public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        // Se o usuario estiver logado, redirecionar para a home
        if(_sessao.BuscarSessaoDoUsuario() != null){ // tem um usuario aberto
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Sair(){
        _sessao.RemoverSessaoUsuario();
        return RedirectToAction("Index", "Login"); // redirecionando para a tela index do login
    }

    [HttpPost]
    public IActionResult Entrar(LoginModel loginModel){
        try
        {
            if(ModelState.IsValid){
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmail(loginModel.Email);

                if(usuario != null){
                    if(usuario.SenhaValida(loginModel.Senha)){
                        _sessao.CriarSessaoDoUsuario(usuario);
                        return RedirectToAction("Index", "Home"); // vai ser redirecionado pro index da home
                    }
                    TempData["MensagemErro"] = $"A senha do usuário é inválida, tente novamente.";
                }
                TempData["MensagemErro"] = $"E-mail e/ou senha inválido(s). Por favor, tente novamente.";
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente. Detalhes do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}