using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using WebApplication1.Repositorio;
using WebApplication1.Helper;

namespace WebApplication1.Controllers;

public class UsuarioController : Controller{
    
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;

    public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos(usuarioLogado.Id);
        return View(usuarios);
    }

    public IActionResult Editar(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Editar(UsuarioModel usuario){
        try{
            if(ModelState.IsValid){
                _usuarioRepositorio.Atualizar(usuario);
                TempData["MensagemSucesso"] = "Usuario atualizado com sucesso!";
                return RedirectToAction("Index");      
            }
            return View("Editar", usuario);
            
        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o usuario, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
        }
    }
    public IActionResult ApagarConfirmacao(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }

    public IActionResult Apagar(int id)
    {   
        try
        {
            bool apagado = _usuarioRepositorio.Apagar(id);
            if(apagado){
                TempData["MensagemSucesso"] = "Usuario apagado com sucesso!";
            }else{
                TempData["MensagemErro"] = "Ops, não conseguimos apagar o usuario, tente novamente.";
            }
            return RedirectToAction("Sair", "Login");
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar o usuario, tente novamente. Mais detalhes do erro: {erro.Message}";
            throw;
        }

    }
}