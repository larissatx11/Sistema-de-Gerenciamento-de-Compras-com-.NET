using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApplication1.Helper;

public class Sessao : ISessao  {
    private readonly IHttpContextAccessor _httpContext;

    public Sessao(IHttpContextAccessor httpContext){
        _httpContext = httpContext;
    }

    public void CriarSessaoDoUsuario(UsuarioModel usuario){
         string valor = JsonConvert.SerializeObject(usuario); // convertendo o objeto em uma string serializada
        _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
    }

    public UsuarioModel BuscarSessaoDoUsuario(){
        string sessaoUsuario =  _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
        if(string.IsNullOrEmpty(sessaoUsuario)){
            return null;
        }else{
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }
    }

    public void RemoverSessaoUsuario(){
        _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
    }

}