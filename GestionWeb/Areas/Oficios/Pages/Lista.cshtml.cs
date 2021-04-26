using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    public class ListaModel : PageModel
    {
        private readonly GestionWeb.Data.GestionOficiosContext _context;

        public ListaModel(GestionWeb.Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<GestionWeb.Data.Oficios> Oficios { get; set; }
        public IList<GestionWeb.Data.TipoOficio> Tipos { get; set; }
        public IList<GestionWeb.Data.Receptores> Receptores{ get; set; }

        public async Task OnGetAsync()
        {

            //foreach (var o in _context.Oficios)
            //{
            //    o.FechaRecepcion = o.OficiosEstados.First(e => e.IdEstado == 1).FechaHora;

            //}
            //await _context.SaveChangesAsync();
            Oficios = await _context.Oficios.Where(o=> !o.Archivado) .OrderByDescending( o=>o.FechaRecepcion).ToListAsync();
            Tipos = await _context.TipoOficio.ToListAsync();
            Receptores = await _context.Receptores.Where(r=>!r.Oculto && r.Departamento == 1 ) .ToListAsync();


        }
    }
}
