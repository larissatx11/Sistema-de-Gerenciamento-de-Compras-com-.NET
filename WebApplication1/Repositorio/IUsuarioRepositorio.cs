using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public interface IUsuarioRepositorio{
    UsuarioModel BuscarPorEmail(string email);
    UsuarioModel ListarPorId(int id);
    List<UsuarioModel> BuscarTodos(int id);
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    bool Apagar(int id);
}