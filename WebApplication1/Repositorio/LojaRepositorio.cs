using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public class LojaRepositorio : ILojaRepositorio{

    private readonly BancoContext _context;

    public LojaRepositorio(BancoContext context)
    {
        _context = context;
    }

    public LojaModel ListarPorId(int id)    
    {
        return _context.Lojas.FirstOrDefault(x => x.Id == id);
    }

    public List<LojaModel> BuscarTodos(int usuarioId){
        return _context.Lojas.Where(x => x.UsuarioId == usuarioId).ToList();
    }

    public LojaModel Adicionar(LojaModel loja){
        try{
            // gravar no banco de dados
            _context.Lojas.Add(loja);
            _context.SaveChanges();
            Console.WriteLine("Loja adicionada com sucesso!");
            return loja;
        }
        catch (Exception ex){
            // Lidar com a exceção ou registrar o erro
            Console.WriteLine($"Erro ao adicionar loja: {ex.Message}");
            throw;
        }
    }
    public LojaModel Atualizar(LojaModel loja)
    {
        LojaModel lojaBD = ListarPorId(loja.Id);
        if(lojaBD == null){
            throw new System.Exception("Houve um erro na atualização da loja!");
        }else{
            lojaBD.Nome = loja.Nome;
            lojaBD.Endereco = loja.Endereco;

            _context.Lojas.Update(lojaBD);
            _context.SaveChanges();

            return lojaBD;
        }
    }

    public bool Apagar(int id)
    {
        LojaModel lojaBD = ListarPorId(id);
        if(lojaBD == null){
            throw new System.Exception("Houve um erro ao deletar a loja!");
        }else{
            _context.Lojas.Remove(lojaBD);
            _context.SaveChanges();
            return true;
        }
    }
}