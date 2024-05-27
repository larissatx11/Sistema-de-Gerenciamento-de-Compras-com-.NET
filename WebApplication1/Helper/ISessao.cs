using WebApplication1.Models;
namespace WebApplication1.Helper;

public interface ISessao
{
    void CriarSessaoDoUsuario(UsuarioModel usuario);
    void RemoverSessaoUsuario();
    UsuarioModel BuscarSessaoDoUsuario();
}