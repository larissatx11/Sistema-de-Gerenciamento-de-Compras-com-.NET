using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using WebApplication1.Repositorio;
using WebApplication1.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication1.Controllers;

public class ComprasController : Controller
{
    private readonly ICompraRepositorio _compraRepositorio;
    private readonly IProdutoRepositorio _produtoRepositorio; 
    private readonly ISessao _sessao;

    public ComprasController(ICompraRepositorio compraRepositorio, ISessao sessao, IProdutoRepositorio produtoRepositorio)
    {
        _compraRepositorio = compraRepositorio;
        _produtoRepositorio = produtoRepositorio;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<CompraModel> compras = _compraRepositorio.BuscarTodos(usuarioLogado.Id);
        return View(compras);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Criar()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<ProdutosModel> produtosDoUsuario = _produtoRepositorio.BuscarTodos(usuarioLogado.Id);

        if (produtosDoUsuario != null && produtosDoUsuario.Count > 0)
        {
            // Popule ViewBag com as lojas do usuário
            ViewBag.Produtos = produtosDoUsuario;
            if (ViewBag.Produtos != null)
            {
               foreach (var produto in ViewBag.Produtos)
                    {
                        Console.WriteLine($"Produto: Nome: {produto.Nome}");
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
        CompraModel compra = _compraRepositorio.ListarPorId(id);
        if (compra == null)
        {
            return NotFound(); // Tratar o caso em que a compra não é encontrada
        }

        // Carregar produtos do usuário para a view
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<ProdutosModel> produtosDoUsuario = _produtoRepositorio.BuscarTodos(usuarioLogado.Id);
        ViewBag.Produtos = produtosDoUsuario;

        return View(compra);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        CompraModel compra = _compraRepositorio.ListarPorId(id);
        return View(compra);
    }
    public IActionResult Apagar(int id)
    {   
        try
        {
            bool apagado = _compraRepositorio.Apagar(id);
            if(apagado){
                TempData["MensagemSucesso"] = "Compra apagada com sucesso!";
            }else{
                TempData["MensagemErro"] = "Ops, não conseguimos apagar sua compra, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos apagar sua compra, tente novamente. Mais detalhes do erro: {erro.Message}";
            throw;
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Criar(CompraModel compra)
    {
        try
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ProdutosModel> produtosDoUsuario = _produtoRepositorio.BuscarTodos(usuarioLogado.Id);
            ViewBag.Produtos = produtosDoUsuario;

            ProdutosModel produtoSelecionado = _produtoRepositorio.ListarPorId(compra.ProdutoId);

            // Atribuir o produto completo ao produto
            compra.Produto = produtoSelecionado;
            compra.UsuarioId = usuarioLogado.Id;
            Console.WriteLine($"Produto selecionado: {compra.Produto.Nome}");

            // Remover a validação da propriedade de navegação
            ModelState.Remove("Produto");
            ModelState.Remove("Usuario");

            // Se o modelo for válido, adicione o produto
            _compraRepositorio.Adicionar(compra);
            produtoSelecionado.Compras.Add(compra);
            TempData["MensagemSucesso"] = "Compra cadastrada com sucesso!";
            return RedirectToAction("Index");
        }
        catch (System.Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar sua compra, tente novamente. Detalhes do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }


    [HttpPost]
    public IActionResult Alterar(CompraModel compra){
        try{
           UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        
            // Carregar produtos do usuário para a view
            List<ProdutosModel> produtosDoUsuario = _produtoRepositorio.BuscarTodos(usuarioLogado.Id);
            ViewBag.Produtos = produtosDoUsuario;

            // Buscar o produto selecionado
            ProdutosModel produtoSelecionado = _produtoRepositorio.ListarPorId(compra.ProdutoId);

            // Atribuir o produto completo à compra
            compra.Produto = produtoSelecionado;
            compra.UsuarioId = usuarioLogado.Id;

            ModelState.Remove("Produto");
            ModelState.Remove("Usuario");
            
            if(ModelState.IsValid){
                _compraRepositorio.Atualizar(compra);
                TempData["MensagemSucesso"] = "Compra atualizada com sucesso!";
                return RedirectToAction("Index");  
            }
            return View("Editar", compra);

        }catch(System.Exception erro){
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar sua compra, tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
        }
    }

}
