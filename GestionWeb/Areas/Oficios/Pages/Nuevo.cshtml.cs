using GestionWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{

    [Authorize]
    public class NuevoModel : PageModel
    {
        private readonly GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NuevoModel(UserManager<IdentityUser> userManager, GestionOficiosContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.Oficios Oficios { get; set; }
        [BindProperty]
        public IList<Usuarios> Usuarios { get; set; }
        [BindProperty]
        public IList<Departamentos> Departamentos { get; set; }

        [BindProperty]
        public IList<TiposDeEmisor> TipoEmisores { get; set; }

        [BindProperty]
        public DateTime? OficiosTerminoFecha { get; set; }

        SessionData SessionUser
        {
            get
            {
                return Core.GetSession(_userManager.GetUserId(User));
            }
        }

        public async Task<IActionResult> OnGet()
        {
            var idDep = SessionUser.IdDepartamento;
            ViewData["IdEmisor"] = new SelectList(_context.Emisores, "Id", "Nombre");
            ViewData["IdReceptor"] = new SelectList(_context.Usuarios.Where(e => !e.Oculto && e.IdDepartamento == idDep), "Id", "Nombre");
            ViewData["IdTipo"] = new SelectList(_context.TipoOficio, "Id", "Nombre");
            TipoEmisores = await _context.TiposDeEmisor.ToListAsync();
            //ViewData["IdTipoEmisor"] = new SelectList(_context.TiposDeEmisor, "Id", "Nombre");
            Usuarios = await _context.Usuarios.Where(e => !e.Oculto && e.IdDepartamento == idDep).ToListAsync();
            Departamentos = await _context.Departamentos.Where(r => r.Id != idDep).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var idDep = SessionUser.IdDepartamento;
            //Usuarios = await _context.Usuarios.Where(e => !e.Oculto && e.IdDepartamento == idDep).ToListAsync();
            //Departamentos = await _context.Departamentos.Where(r => r.Id != idDep).ToListAsync();

            Oficios.IdDepartamento = SessionUser.IdDepartamento;
            Oficios.FechaRecepcion = DateTime.Now;
            
            Oficios.UltimoEstado = EstadoOficio.Capturado;
            Oficios.IdUsuario = Oficios.IdReceptor;

            Oficios = _context.Oficios.Add(Oficios).Entity;

            await _context.SaveChangesAsync();

            if (OficiosTerminoFecha != null)
            {
                var ot = new OficiosTermino
                {
                    Fecha = OficiosTerminoFecha.Value,
                    Id = Oficios.Id
                };

                _context.OficiosTermino.Add(ot);
                await _context.SaveChangesAsync();

            }

            var r = Request.Form["TurnarA"][0];
            _context.OficiosEstados.Add(new OficiosEstados()
            {
                FechaHora = DateTime.Now,
                IdEstado = EstadoOficio.Capturado,
                IdOficio = Oficios.Id,
                IdUsuario = Oficios.IdReceptor
            });
            

            if (r != "0")
            {

                var value = r.Split('-');
                //TODO: validar que sea un valor aceptable

                if (value[0] == "u")
                {
                    var idu = Convert.ToInt32(value[1]);
                    var x = await Controllers.Oficios.TurnarOficio(Oficios.Id, idu, Oficios.IdReceptor);
                    
                    Oficios.UltimoEstado = EstadoOficio.PendienteRecibir;
                    Oficios.IdUsuario = idu;
                    await _context.SaveChangesAsync();

                    if (x.ToLower().Contains("archivo"))
                    {
                        Oficios.Archivado = true;
                        await _context.SaveChangesAsync();
                    }
                }
                else if (value[0] == "d")
                {
                    //turnar a un departamento
                }
            }
            //TODO: subir archivos al contenedor
            if (Request.Form.Files.Any())
            {

            }

            //var archivo = Request.Form.Files;

            return RedirectToPage("/Oficios/Nuevo");
        }
    }
}
