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

        public IActionResult Marcar(int id)
        {
            var action = Convert.ToInt32( Request.Query["action"]);

            switch (action)
            {
                case 2:  //turnar
                    {
                        break;
                    }
                case 3: //Aceptar en bandeja
                    {
                        break;
                    }
                case 4: //enviar contestado
                    {
                        break;
                    }
                case 5: //confirmar contestado
                    {
                        break;
                    }
                case 7: //archivar
                    {
                        break;
                    }
                case 9: //eliminar
                    {
                        break;
                    }
            }

            return Redirect("/Oficios/Detalles?id="+id);
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

        public string QuitarComentario(int id)
        {
            // este proceso solo debé hacerse 
            return "";
        }

    }
}
