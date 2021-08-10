using GestionWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionWeb.Controllers
{
    public class Oficios : Controller
    {
        private readonly GestionOficiosContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Oficios(UserManager<IdentityUser> userManager, GestionOficiosContext context)
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

        public async Task<ActionResult> Marcar(int id)
        {
            var action = (EstadoOficio)Convert.ToByte(Request.Query["action"]);
            var oficio = _context.Oficios.First(o => o.Id == id);

            switch (action)
            {
                case EstadoOficio.PendienteRecibir:  //turnar
                    {
                        break;
                    }
                case EstadoOficio.EnMiPoder: //Aceptar en bandeja
                    {
                        break;
                    }
                case EstadoOficio.EnviadoParaConfirmar: //enviar contestado
                    {
                        var estado = new OficiosEstados()
                        {
                            FechaHora = DateTime.Now,
                            IdEstado = EstadoOficio.EnviadoParaConfirmar,
                            IdOficio = oficio.Id,
                            IdUsuario = SessionUser.IdUsuario

                        };
                        estado = _context.OficiosEstados.Add(estado).Entity;
                        await _context.SaveChangesAsync();
                        return Redirect("/Oficios/MisOficios");
                    }
                case EstadoOficio.Contestado: //confirmar contestado
                    {
                        var estado = new OficiosEstados()
                        {
                            FechaHora = DateTime.Now,
                            IdEstado = EstadoOficio.Contestado,
                            IdOficio = oficio.Id,
                            IdUsuario = SessionUser.IdUsuario

                        };
                        estado = _context.OficiosEstados.Add(estado).Entity;

                        //Poner en pendiente de archivar.

                        await _context.SaveChangesAsync();
                        return Redirect("/Oficios/oficiospendientes");
                    }
                case EstadoOficio.Archivado: //archivar
                    {
                        oficio.Archivado = true;
                        //var estado = new OficiosEstados()
                        //{
                        //    FechaHora = DateTime.Now,
                        //    IdEstado = EstadoOficio.Contestado,
                        //    IdOficio = oficio.Id,
                        //    IdUsuario = SessionUser.IdUsuario

                        //};
                        //estado = _context.OficiosEstados.Add(estado).Entity;

                        ////Poner en pendiente de archivar.

                        await _context.SaveChangesAsync();
                        //return Redirect("/Oficios/oficiospendientes");
                        break;
                    }
                case EstadoOficio.Eliminado: //eliminar
                    {
                        _context.OficiosEstados.RemoveRange(oficio.OficiosEstados);                        
                        _context.Oficios.Remove(oficio);

                        await _context.SaveChangesAsync();
                        return Redirect("/Oficios");
                    }
            }

            return Redirect("/Oficios/Detalles?id=" + id);
        }

        public string AgregarComentario(int id)
        {
            try
            {
                var r = Request.Query["text"];
                var nota = new OficiosEstadosNotas();
                nota.FechaHora = DateTime.Now;
                nota.Nota = r;
                nota.IdEstadoOficio = id;
                _context.OficiosEstadosNotas.Add(nota);
                _context.SaveChanges();
                return nota.FechaHora.ToString();
            }
            catch
            {
                return "error";
            }
        }

        public async Task<ActionResult> TurnarOficio(int id)
        {
            try
            {
                var user = Request.Query["user"].First();
                var u = Convert.ToInt32(user);
                var x = await turnarOficio(id, u, SessionUser.IdUsuario, "");
                return Ok(x);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<ActionResult> CrearNuevoEmisor()
        {
            var nombre = Request.Query["nombre"].First();
            var tipo = short.Parse(Request.Query["tipo"].First());

            var em = _context.Emisores.Add(new Emisores() { Nombre = nombre, IdTipoEmisor = tipo }).Entity;
            try
            {
                await _context.SaveChangesAsync();
                return Json(em.Id);
            }
            catch
            {
                return Json("error");
            }
        }

        public async Task<ActionResult> CrearNuevoTipoOficio()
        {
            var nombre = Request.Query["nombre"].First();
            var em = _context.TipoOficio.Add(new TipoOficio() { Nombre = nombre }).Entity;
            try
            {
                await _context.SaveChangesAsync();
                return Json(em.Id);
            }
            catch
            {
                return Json("error");
            }
        }

        public async Task<ActionResult> ArchivarOficio(int id)
        {
            try
            {
                var idCajon = Convert.ToInt16(Request.Query["caja"].First());
                var archivo = new OficiosArchivado()
                {
                    Id = id,
                    IdCajon = idCajon
                };

                _context.OficiosArchivado.Add(archivo);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        public static async Task<string> turnarOficio(int OficiosId, int IdUsuario, int OficiosIdReceptor, string text = "desde recepción ")
        {
            var _context = new GestionOficiosContext();

            var user = new OficiosUsuarios
            {
                IdOficio = OficiosId,
                IdUsuario = IdUsuario
            };
            user = _context.OficiosUsuarios.Add(user).Entity;
            var nom = _context.Usuarios.First(u => u.Id == user.IdUsuario).Nombre;
          
            var estado = new OficiosEstados()
            {
                FechaHora = DateTime.Now,
                IdEstado = EstadoOficio.PendienteRecibir,
                IdOficio = OficiosId,
                IdUsuario = OficiosIdReceptor

            };
            estado = _context.OficiosEstados.Add(estado).Entity;
            await _context.SaveChangesAsync();

            if (!nom.ToLower().Contains("archivo"))
            {
                _context.OficiosEstadosNotas.Add(new OficiosEstadosNotas()
                {
                    FechaHora = DateTime.Now,
                    IdEstadoOficio = estado.Id,
                    Nota = "Turnado " + text + "a " + nom
                });
                await _context.SaveChangesAsync();
            }

            return nom;
        }

        public string QuitarComentario(int id)
        {
            // este proceso solo debé hacerse 
            return "";
        }

        public IActionResult PUSHSuscribe(string client, string endpoint, string p256dh, string auth)
        {
            //if (client == null)
            //{
            //    return BadRequest("No Client Name parsed.");
            //}
            //if (PersistentStorage.GetClientNames().Contains(client))
            //{
            //    return BadRequest("Client Name already used.");
            //}
            //var subscription = new PushSubscription(endpoint, p256dh, auth);
            //PersistentStorage.SaveSubscription(client, subscription);
            //return View("Notify", PersistentStorage.GetClientNames());
            return NotFound();
        }
    }
}
