using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    public class MisOficiosModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MisOficiosModel(UserManager<IdentityUser> userManager, Data.GestionOficiosContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }

        public IList<Data.Oficios> Oficios { get; set; }

        public async Task OnGetAsync(int? id)
        {
            bool EnArchivo = id != null;

            var yo = await _context.Usuarios.FirstAsync(u=>u.Id == SessionUser.IdUsuario);
            Oficios = yo.OficiosUsuarios.Select(o => o.IdOficioNavigation).Where(o => o.Archivado == EnArchivo).OrderByDescending(o => o.FechaRecepcion).ToList();
        }
    }
}
