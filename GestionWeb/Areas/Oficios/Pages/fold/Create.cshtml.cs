using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionWeb.Data;

namespace GestionWeb.Areas_Oficios_Pages_fold
{
    public class CreateModel : PageModel
    {
        private readonly GestionWeb.Data.GestionOficiosContext _context;

        public CreateModel(GestionWeb.Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
        ViewData["IdEmisor"] = new SelectList(_context.Emisores, "Id", "Nombre");
        ViewData["IdReceptor"] = new SelectList(_context.Usuarios, "Id", "AspNetId");
        ViewData["IdTipo"] = new SelectList(_context.TipoOficio, "Id", "Nombre");
        ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "AspNetId");
            return Page();
        }

        [BindProperty]
        public Oficios Oficios { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Oficios.Add(Oficios);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
