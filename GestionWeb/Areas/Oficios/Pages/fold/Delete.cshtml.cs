using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionWeb.Data;

namespace GestionWeb.Areas_Oficios_Pages_fold
{
    public class DeleteModel : PageModel
    {
        private readonly GestionWeb.Data.GestionOficiosContext _context;

        public DeleteModel(GestionWeb.Data.GestionOficiosContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Oficios = await _context.Oficios.FindAsync(id);

            if (Oficios != null)
            {
                _context.Oficios.Remove(Oficios);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
