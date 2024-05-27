using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public interface ILojaRepositorio{
    LojaModel ListarPorId(int id);
    List<LojaModel> BuscarTodos(int usuarioId);
    LojaModel Adicionar(LojaModel loja);
    LojaModel Atualizar(LojaModel loja);
    bool Apagar(int id);
}