using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Repositorio;
using WebApplication1.Models;

namespace WebApplication1.Repositorio;

public interface IProdutoRepositorio{
    ProdutosModel ListarPorId(int id);
    List<ProdutosModel> BuscarTodos(int usuarioId);
    ProdutosModel Adicionar(ProdutosModel produto);
    ProdutosModel Atualizar(ProdutosModel produto);
    bool Apagar(int id);
}