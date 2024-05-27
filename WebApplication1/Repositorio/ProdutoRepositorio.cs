using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositorio;

public class ProdutoRepositorio : IProdutoRepositorio{

    private readonly BancoContext _context;

    public ProdutoRepositorio(BancoContext context)
    {
        _context = context;
    }

    public ProdutosModel ListarPorId(int id)    
    {
        return _context.Produtos.FirstOrDefault(x => x.Id == id);
    }

    public LojaModel BuscarLojaPorId(int lojaId){
        return _context.Lojas.FirstOrDefault(x => x.Id == lojaId);
    }

    public List<ProdutosModel> BuscarTodos(int usuarioId){
        return _context.Produtos.Where(x => x.UsuarioId == usuarioId).Include(x => x.Loja).ToList();
    }
    
    public ProdutosModel Adicionar(ProdutosModel produto){
        try{
            // gravar no banco de dados
            Console.WriteLine($"Adicionando produto: {produto.Nome}, {produto.Preco}, {produto.LojaId}, {produto.UsuarioId}");
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            Console.WriteLine("Produto adicionado com sucesso!");
            return produto;
        }
        catch (Exception ex){
            // Lidar com a exceção ou registrar o erro
            Console.WriteLine($"Erro ao adicionar produto: {ex.Message}");
            throw;
        }
    }
    public ProdutosModel Atualizar(ProdutosModel produto)
    {
        ProdutosModel produtoBD = ListarPorId(produto.Id);
        if(produtoBD == null){
            throw new System.Exception("Houve um erro na atualização do produto!");
        }else{
            produtoBD.Nome = produto.Nome;
            produtoBD.Preco = produto.Preco;
            produtoBD.LojaId = produto.LojaId;

            _context.Produtos.Update(produtoBD);
            _context.SaveChanges();

            return produtoBD;
        }
    }

    public bool Apagar(int id)
    {
        ProdutosModel produtoBD = ListarPorId(id);
        if(produtoBD == null){
            throw new System.Exception("Houve um erro ao deletar o produto!");
        }else{
            _context.Produtos.Remove(produtoBD);
            _context.SaveChanges();
            return true;
        }
    }
}