using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IList<Data.Usuarios> Usuarios { get; set; }
        public IList<Data.Departamentos> Departamentos { get; set; }

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
            ViewData["IdReceptor"] = new SelectList(_context.Receptores.Where(e => !e.Oculto && e.Departamento == idDep ), "Id", "Nombre");
            ViewData["IdTipo"] = new SelectList(_context.TipoOficio, "Id", "Nombre");
            Usuarios = await _context.Usuarios.Where(r => !r.Oculto && r.IdDepartamento == idDep).ToListAsync();
            Departamentos = await _context.Departamentos.Where(r => r.Id!= idDep).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Oficios.IdDepartamento = SessionUser.IdDepartamento;
            Oficios.FechaRecepcion = DateTime.Now;
            Oficios = _context.Oficios.Add(Oficios).Entity;
            await _context.SaveChangesAsync();

            var r = Request.Form["TurnarA"][0];
            _context.OficiosEstados.Add(new OficiosEstados()
            {
                FechaHora = DateTime.Now,
                IdEstado = 1,
                IdOficio = Oficios.Id,
                IdUsuario = Oficios.IdReceptor

            });

            if (r != "0")
            {

                var value = r.Split('-');
                //TODO: validar que sea un valor aceptable



                if (value[0] == "u")
                {
                    var user = new OficiosUsuarios
                    {
                        IdOficio = Oficios.Id,
                        IdUsuario = Convert.ToInt32(value[1])
                    };
                    user = _context.OficiosUsuarios.Add(user).Entity;
                    var estado = new OficiosEstados()
                    {
                        FechaHora = DateTime.Now,
                        IdEstado = 2,
                        IdOficio = Oficios.Id,
                        IdUsuario = SessionUser.IdUsuario

                    };
                    estado = _context.OficiosEstados.Add(estado).Entity;
                    await _context.SaveChangesAsync();

                    _context.OficiosEstadosNotas.Add(new OficiosEstadosNotas()
                    {
                        FechaHora = DateTime.Now,
                        IdEstadoOficio = estado.Id,
                        Nota = "Turnado desde recepci�n a " + _context.Usuarios.First(u => u.Id == user.IdUsuario).Nombre
                    });
                    await _context.SaveChangesAsync();
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
