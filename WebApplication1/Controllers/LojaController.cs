using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using WebApplication1.Repositorio;
using WebApplication1.Helper;

namespace WebApplication1.Controllers;

public class LojaController : Controller
{
    private readonly ILojaRepositorio _lojaRepositorio;
    private readonly ISessao _sessao;
    public LojaController(ILojaRepositorio lojaRepositorio, ISessao sessao)
    {
        _lojaRepositorio = lojaRepositorio;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<LojaModel> lojas = _lojaRepositorio.BuscarTodos(usuarioLogado.Id);

        return View(lojas);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Criar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        LojaModel loja = _lojaRepositorio.ListarPorId(id);
        return View(loja);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        LojaModel loja = _lojaRepositorio.ListarPorId(id);
        return View(loja);
    }

    public IActionResult Apagar(int id)
    {   
        try
        {
            bool apagado = _lojaRepositorio.Apagar(id);
            if(apagado){
                TempData["MensagemSucesso"] = "Loja apagada com sucesso!";
            }else{
                TempData["MensagemErro"] = "Ops, não conseguimos apagar sua loja, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar sua loja, tente novamente. Mais detalhes do erro: {erro.Message}";
            throw;
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Criar(LojaModel loja){
        try{
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                loja.UsuarioId = usuarioLogado.Id;
                // Atribuir Produtos (vazio inicialmente, pois estamos criando uma nova loja)
                loja.Produtos = new List<ProdutosModel>();
                ModelState.Remove("Usuario");
                ModelState.Remove("Produtos");

                if (ModelState.IsValid){
                    //loja.Usuario = usuarioLogado;
                    _lojaRepositorio.Adicionar(loja);
                    usuarioLogado.Lojas.Add(loja);
                    TempData["MensagemSucesso"] = "Loja cadastrada com sucesso!";
                    return RedirectToAction("Index");
                }
                
            return View(loja);

        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar sua loja, tente novamente. Detalhes do erro: {erro.Message}";
                 Console.WriteLine($"Erro: {erro.Message}");
                return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Alterar(LojaModel loja){
        try{
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                loja.UsuarioId = usuarioLogado.Id;
                ModelState.Remove("Usuario");
                ModelState.Remove("Produtos");
                if (ModelState.IsValid){
                    _lojaRepositorio.Atualizar(loja);
                    TempData["MensagemSucesso"] = "Loja atualizada com sucesso!";
                    return RedirectToAction("Index");  
                }    
                return View("Editar", loja);
        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar sua loja, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
        }
    }

}
