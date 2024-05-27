using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public class UsuarioRepositorio : IUsuarioRepositorio{

    private readonly BancoContext _context;

    public UsuarioRepositorio(BancoContext context)
    {
        _context = context;
    }
    public UsuarioModel BuscarPorEmail(string email)
    {
        return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
    }

    public UsuarioModel ListarPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(x => x.Id == id);
    }

    public List<UsuarioModel> BuscarTodos(int usuarioId){
        return _context.Usuarios.Where(x => x.Id == usuarioId).ToList();
    }
    
    public UsuarioModel Adicionar(UsuarioModel usuario){
        try{
            // gravar no banco de dados
            Console.WriteLine($"Adicionando Usuario: {usuario.Nome}, {usuario.Email}, {usuario.Senha}");
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            Console.WriteLine("Usuario adicionado com sucesso!");
            return usuario;
        }
        catch (Exception ex){
            // Lidar com a exceção ou registrar o erro
            Console.WriteLine($"Erro ao adicionar usuario: {ex.Message}");
            throw;
        }
    }
    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        UsuarioModel usuarioBD = ListarPorId(usuario.Id);
        if(usuarioBD == null){
            throw new System.Exception("Houve um erro na atualização do usuario!");
        }else{
            usuarioBD.Nome = usuario.Nome;
            usuarioBD.Email = usuario.Email;
            usuarioBD.Senha = usuario.Senha;

            _context.Usuarios.Update(usuarioBD);
            _context.SaveChanges();

            return usuarioBD;
        }
    }

    public bool Apagar(int id)
    {
        UsuarioModel usuarioBD = ListarPorId(id);
        if(usuarioBD == null){
            throw new System.Exception("Houve um erro ao deletar o usuario!");
        }else{
            _context.Usuarios.Remove(usuarioBD);
            _context.SaveChanges();
            return true;
        }
    }
}