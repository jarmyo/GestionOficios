using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class DetallesModel : PageModel
    {
        private readonly GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetallesModel(UserManager<IdentityUser> userManager, GestionOficiosContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }
        public Data.Oficios Oficios { get; set; }
        public IList<Data.Usuarios> Usuarios { get; set; }
        public EstadoOficio MiEstadoOficio;

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
                .Include(o => o.IdTipoNavigation).FirstOrDefaultAsync(m => m.Id == id);


            //Determinar en que estado se encuentra uno 
            if (Oficios == null)
            {
                return NotFound();
            }

            var misestados = Oficios.OficiosEstados.Where(o => o.IdUsuario == SessionUser.IdUsuario);
            if (misestados.Any())
            {
                var ultimo = misestados.OrderByDescending(o=>o.IdEstado).ThenBy(o => o.FechaHora) .First();

                MiEstadoOficio = (EstadoOficio)ultimo.IdEstado;
                //ultimo estado.
            }
            else
            {
                MiEstadoOficio = EstadoOficio.NoEsMio;
            }

            Usuarios =  await _context.Usuarios.Where(u => u.IdDepartamento == SessionUser.IdDepartamento && u.Oculto == false).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Cambiar el estado del oficio

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


            return Page();
        }
    }


    public enum EstadoOficio
    {
        NoEsMio = 0,
        Capturado = 1,
        PendienteRecibir = 2,
        EnMiPoder = 3,
        EnviadoParaConfirmar=4,
        Contestado = 5,
        Archivado = 7,
        Eliminado = 9
    }
}
