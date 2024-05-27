using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using WebApplication1.Repositorio;
using WebApplication1.Helper;

namespace WebApplication1.Controllers;

public class CadastrarController : Controller{

    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;

    public CadastrarController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Login");
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(UsuarioModel usuario){
        try{
            if(ModelState.IsValid){
                _usuarioRepositorio.Adicionar(usuario);
                TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                return RedirectToAction("Index", "Login");
            }
            return View(usuario);
        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuario, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index", "Login");
        }
    }
}