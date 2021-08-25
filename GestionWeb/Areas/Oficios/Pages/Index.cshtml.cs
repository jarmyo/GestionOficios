using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string NumeroOficiosPendientes { get; set; } = string.Empty;
        [BindProperty]
        public IList<Data.Usuarios> Usuarios { get; set; }
        private readonly Data.GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;        

        public IndexModel(UserManager<IdentityUser> userManager, Data.GestionOficiosContext context)
        {
            _userManager = userManager;
            _context = context;            
        }
        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //foreach (var oficio in _context.Oficios)
            //{
            //    if (oficio.OficiosEstados.Any())
            //    {
            //        Debug.Write(".");
            //        var ultimoEstado = oficio.OficiosEstados.OrderByDescending(o => o.FechaHora).First();
            //        oficio.UltimoEstado = ultimoEstado.IdEstado;
            //        oficio.IdUsuario = ultimoEstado.IdUsuario;
            //    }
            //    else
            //    {
            //        Debug.WriteLine("-");
            //    }
            //}
            //await _context.SaveChangesAsync();

            if (User.IsInRole("Supervisor") || User.IsInRole("Director"))
            {
                Usuarios = await _context.Usuarios.Where(u => u.IdDepartamento == SessionUser.IdDepartamento && u.Oculto == false).ToListAsync();
                int count = 0;
                foreach (var _ in from oficio in _context.Oficios
                                  where oficio.Archivado
                                  where oficio.OficiosArchivado == null
                                  select new { })
                {
                    count += 1;
                }

                foreach (var oe in _context.OficiosEstados.Where(o => o.IdEstado == EstadoOficio.EnviadoParaConfirmar))
                {
                    count += 1;
                }

                if (count > 0)
                {
                    NumeroOficiosPendientes = count.ToString();
                }
            }

            return Page();
        }
    }
}
