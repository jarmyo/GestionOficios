using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class FechaModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;

        public FechaModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> Oficios { get; set; }
      //  public bool MostrarArchivados { get; set; }

        public async Task OnGetAsync(string id)
        {
            //    MostrarArchivados = id != null;

            var date = DateTime.ParseExact(id, "dd/MM/yyyy", new CultureInfo("es-MX"));
            ViewData["Title"] = "Oficios de la fecha  " + date.ToShortTimeString();
            Oficios = await _context.Oficios.Where(o => o.FechaRecepcion.Date == date).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
        }
    }
}
