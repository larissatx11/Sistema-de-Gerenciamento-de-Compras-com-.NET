using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using WebApplication1.Repositorio;
using WebApplication1.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication1.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    private readonly ILojaRepositorio _lojaRepositorio; 
    private readonly ISessao _sessao;

    public ProdutosController(IProdutoRepositorio produtoRepositorio, ISessao sessao, ILojaRepositorio lojaRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
        _lojaRepositorio = lojaRepositorio;
        _sessao = sessao;
    }
    public IActionResult Index()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<ProdutosModel> produtos = _produtoRepositorio.BuscarTodos(usuarioLogado.Id);

        return View(produtos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Criar()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        Console.WriteLine($"Usuário Logado: {usuarioLogado?.Id} - {usuarioLogado?.Nome}");
        List<LojaModel> lojasDoUsuario = _lojaRepositorio.BuscarTodos(usuarioLogado.Id);

        if (lojasDoUsuario != null && lojasDoUsuario.Count > 0)
        {
            // Popule ViewBag com as lojas do usuário
            ViewBag.Lojas = lojasDoUsuario;
            if (ViewBag.Lojas != null)
            {
               foreach (var loja in ViewBag.Lojas)
                    {
                        Console.WriteLine($"Loja: Nome: {loja}");
                    }

            }
            else
            {
                Console.WriteLine($"Lista de loja vazia");
            }

        }
        else
        {
            TempData["MensagemErro"] = "Não foi possível encontrar lojas associadas ao usuário.";
        }
        return View();
    }

    public IActionResult Editar(int id)
    {
        ProdutosModel produto = _produtoRepositorio.ListarPorId(id);
        if (produto == null)
        {
            return NotFound(); // Tratar o caso em que o produto não é encontrada
        }
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<LojaModel> lojasDoUsuario = _lojaRepositorio.BuscarTodos(usuarioLogado.Id);
        ViewBag.Lojas = lojasDoUsuario;
        return View(produto);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        ProdutosModel produto = _produtoRepositorio.ListarPorId(id);
        return View(produto);
    }

    public IActionResult Apagar(int id)
    {   
        try
        {
            bool apagado = _produtoRepositorio.Apagar(id);
            if(apagado){
                TempData["MensagemSucesso"] = "Produto apagado com sucesso!";
            }else{
                TempData["MensagemErro"] = "Ops, não conseguimos apagar seu produto, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu produto, tente novamente. Mais detalhes do erro: {erro.Message}";
            throw;
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Criar(ProdutosModel produto)
    {
        try
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<LojaModel> lojasDoUsuario = _lojaRepositorio.BuscarTodos(usuarioLogado.Id);
            ViewBag.Lojas = lojasDoUsuario;
            // Buscar a loja correspondente ao LojaId
            LojaModel lojaSelecionada = _lojaRepositorio.ListarPorId(produto.LojaId);

            // Atribuir a Loja completa ao produto
            produto.Loja = lojaSelecionada;
            produto.UsuarioId = usuarioLogado.Id;

            // Atribuir Compras (vazio inicialmente, pois estamos criando um novo produto)
            produto.Compras = new List<CompraModel>();

            // Remover a validação da propriedade de navegação
            ModelState.Remove("Loja");
            ModelState.Remove("Usuario");
            ModelState.Remove("Compras");
            
            if (ModelState.IsValid){
                // Se o modelo for válido, adicione o produto
                _produtoRepositorio.Adicionar(produto);
                lojaSelecionada.Produtos.Add(produto);
                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index");
            }else{
                Console.WriteLine($"Nao foi possivel cadastrar: usuId {produto.UsuarioId} - LojaId {produto.LojaId}");
            }
            return View(produto);
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu produto, tente novamente. Detalhes do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }


    [HttpPost]
    public IActionResult Alterar(ProdutosModel produto){
        try{
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<LojaModel> lojasDoUsuario = _lojaRepositorio.BuscarTodos(usuarioLogado.Id);
            ViewBag.Lojas = lojasDoUsuario;
            // Buscar a loja correspondente ao LojaId
            LojaModel lojaSelecionada = _lojaRepositorio.ListarPorId(produto.LojaId);

            // Atribuir a Loja completa ao produto
            produto.Loja = lojaSelecionada;
            produto.UsuarioId = usuarioLogado.Id;

            ModelState.Remove("Loja");
            ModelState.Remove("Usuario");
            ModelState.Remove("Compras");

            if(ModelState.IsValid){
                _produtoRepositorio.Atualizar(produto);
                lojaSelecionada.Produtos.Add(produto);
                TempData["MensagemSucesso"] = "Produto atualizado com sucesso!";
                return RedirectToAction("Index");      
            }
            return View("Editar", produto);
            
        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu produto, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
        }
    }

}
