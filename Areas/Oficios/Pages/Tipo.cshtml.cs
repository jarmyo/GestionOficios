using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class TipoModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;

        public TipoModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> Oficios { get; set; }
        //public bool MostrarArchivados { get; set; }

        public async Task OnGetAsync(int? id)
        {
                ViewData["Title"] = "Oficios de tipo " + _context.TipoOficio.First(t=>t.Id == id).Nombre ;  
            //  MostrarArchivados = id != null;

            // await _context.SaveChangesAsync();
            Oficios = await _context.Oficios.Where(o => o.IdTipo ==id).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
        }
    }
}