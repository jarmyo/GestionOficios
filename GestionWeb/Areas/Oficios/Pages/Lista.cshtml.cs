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
        private readonly Data.GestionOficiosContext _context;

        public ListaModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> Oficios { get; set; }
        public IList<Data.TipoOficio> Tipos { get; set; }
        public IList<Data.Receptores> Receptores { get; set; }
        public bool MostrarArchivados { get; set; }
        public async Task OnGetAsync(int? id)
        {
            //id servira para checar si es 
            //foreach (var o in _context.Oficios)
            //{
            //    if (o.FechaTermino.HasValue)
            //    {

            //        var ot = new Data.OficiosTermino();
            //        ot.Id = o.Id;
            //        ot.Fecha = o.FechaTermino.Value;
            //        _context.OficiosTermino.Add(ot);
            //    }
            //}

            MostrarArchivados  = id != null;

            await _context.SaveChangesAsync();
            Oficios = await _context.Oficios.Where(o=> o.Archivado== MostrarArchivados) .OrderByDescending( o=>o.FechaRecepcion).ToListAsync();
            Tipos = await _context.TipoOficio.ToListAsync();
            Receptores = await _context.Receptores.Where(r=>!r.Oculto && r.Departamento == 1 ) .ToListAsync();


        }
    }
}
