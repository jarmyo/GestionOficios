using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class OficiosPendientesModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;

        public OficiosPendientesModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> OficiosSinArchivo { get; set; }
        public IList<Data.Oficios> OficiosSinConfirmacion { get; set; }
        public IList<Data.Archiveros> Archiveros { get; set; }

        public async Task OnGetAsync()
        { 
            OficiosSinArchivo = await _context.Oficios.Where(o => o.Archivado && o.OficiosArchivado == null).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
            OficiosSinConfirmacion = await _context.OficiosEstados.Where(e => e.IdEstado == EstadoOficio.EnviadoParaConfirmar).Select(o => o.IdOficioNavigation).ToListAsync();
            Archiveros = await _context.Archiveros.Where(r => r.IdDepartamento == 1).ToListAsync();
        }
    }
}
