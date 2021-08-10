using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionWeb.Data;

namespace GestionWeb.Areas_Oficios_Pages_fold
{
    public class DetailsModel : PageModel
    {
        private readonly GestionWeb.Data.GestionOficiosContext _context;

        public DetailsModel(GestionWeb.Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public Oficios Oficios { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Oficios = await _context.Oficios
                .Include(o => o.IdDepartamentoNavigation)
                .Include(o => o.IdEmisorNavigation)
                .Include(o => o.IdReceptorNavigation)
                .Include(o => o.IdTipoNavigation)
                .Include(o => o.IdUsuarioNavigation).FirstOrDefaultAsync(m => m.Id == id);

            if (Oficios == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
