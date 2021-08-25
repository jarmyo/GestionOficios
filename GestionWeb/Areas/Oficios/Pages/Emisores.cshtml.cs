using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class EmisoresModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;

        public EmisoresModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> Oficios { get; set; }
        //public bool MostrarArchivados { get; set; }

        public async Task OnGetAsync(int? id)
        {
            //MostrarArchivados = id != null;
            ViewData["Title"] = "Oficios emitidos por " + _context.Emisores.First(t => t.Id == id).Nombre;            
            Oficios = await _context.Oficios.Where(o => o.IdEmisor == id).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
        }
    }
}