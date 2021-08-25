using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionWeb
{

    public class Development : Controller
    {        
        private readonly UserManager<IdentityUser> _userManager;

        public Development(UserManager<IdentityUser> userManager)
        {            
            _userManager = userManager;
        }
        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }

        public IActionResult Index()
        {

            return NotFound();
        }

        public IActionResult CambiaUsuario(int id)
        {
            SessionUser.IdUsuario = id;
            return Redirect("~/Oficios/MisOficios");
        }
        public IActionResult CambiaUsuarioArchivo(int id)
        {
            SessionUser.IdUsuario = id;
            return Redirect("~/Oficios/MisOficiosArchivados");
        }
    }
}
