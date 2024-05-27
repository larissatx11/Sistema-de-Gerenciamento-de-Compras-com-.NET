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

public class CompraRepositorio : ICompraRepositorio{

    private readonly BancoContext _context;

    public CompraRepositorio(BancoContext context)
    {
        _context = context;
    }

    public CompraModel ListarPorId(int id)    
    {
        return _context.Compras.FirstOrDefault(x => x.Id == id);
    }

    public LojaModel BuscarLojaPorId(int lojaId){
        return _context.Lojas.FirstOrDefault(x => x.Id == lojaId);
    }

    public List<CompraModel> BuscarTodos(int usuarioId){
        return _context.Compras.Where(x => x.UsuarioId == usuarioId).Include(x => x.Produto).ThenInclude(p => p.Loja).ToList();
    }
    
    public CompraModel Adicionar(CompraModel compra){
        try{
            // gravar no banco de dados
            _context.Compras.Add(compra);
            _context.SaveChanges();
            Console.WriteLine("Compra adicionado com sucesso!");
            return compra;
        }
        catch (Exception ex){
            // Lidar com a exceção ou registrar o erro
            Console.WriteLine($"Erro ao adicionar compra: {ex.Message}");
            throw;
        }
    }
    public CompraModel Atualizar(CompraModel compra)
    {
        CompraModel compraBD = ListarPorId(compra.Id);
        if(compraBD == null){
            throw new System.Exception("Houve um erro na atualização da compra!");
        }else{
            compraBD.ProdutoId = compra.ProdutoId;

            _context.Compras.Update(compraBD);
            _context.SaveChanges();

            return compraBD;
        }
    }

    public bool Apagar(int id)
    {
        CompraModel compraBD = ListarPorId(id);
        if(compraBD == null){
            throw new System.Exception("Houve um erro ao deletar a compra!");
        }else{
            _context.Compras.Remove(compraBD);
            _context.SaveChanges();
            return true;
        }
    }
}