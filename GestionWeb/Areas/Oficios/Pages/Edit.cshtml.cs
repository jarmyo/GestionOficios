using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionWeb.Data;

namespace GestionWeb.Areas.Oficios.Pages

{
    public class EditModel : PageModel
    {
        private readonly GestionWeb.Data.GestionOficiosContext _context;

        public EditModel(GestionWeb.Data.GestionOficiosContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Oficios Oficios { get; set; }

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
           ViewData["IdEmisor"] = new SelectList(_context.Emisores, "Id", "Nombre");           
           ViewData["IdTipo"] = new SelectList(_context.TipoOficio, "Id", "Nombre");           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Oficios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OficiosExists(Oficios.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Detalles", new { id = Oficios.Id });
        }

        private bool OficiosExists(int id)
        {
            return _context.Oficios.Any(e => e.Id == id);
        }
    }
}
