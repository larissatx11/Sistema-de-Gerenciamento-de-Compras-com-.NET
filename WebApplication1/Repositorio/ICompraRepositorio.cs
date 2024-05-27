using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public interface ICompraRepositorio{
    CompraModel ListarPorId(int id);
    List<CompraModel> BuscarTodos(int usuarioId);
    CompraModel Adicionar(CompraModel compra);
    CompraModel Atualizar(CompraModel compra);
    bool Apagar(int id);
}