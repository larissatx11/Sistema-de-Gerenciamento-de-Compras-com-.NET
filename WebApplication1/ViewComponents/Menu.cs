using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models;
using Newtonsoft.Json;

namespace WebApplication1.ViewComponents;

public class Menu : ViewComponent{
    public async Task<IViewComponentResult> InvokeAsync(){
        string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
        if(string.IsNullOrEmpty(sessaoUsuario)){
            return null;
        }
        UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        return View(usuario);
    }
}
