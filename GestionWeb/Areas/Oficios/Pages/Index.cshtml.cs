using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string NumeroOficiosPendientes { get; set; } = string.Empty;
        private readonly Data.GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, Data.GestionOficiosContext context)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }
        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }

        public void OnGet()
        {
            if (User.IsInRole("Supervisor") || User.IsInRole("Director"))
            {
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
        }
    }
}
