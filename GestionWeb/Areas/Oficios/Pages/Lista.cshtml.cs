using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionWeb.Areas.Oficios.Pages
{
    [Authorize]
    public class ListaModel : PageModel
    {
        private readonly Data.GestionOficiosContext _context;

        public ListaModel(Data.GestionOficiosContext context)
        {
            _context = context;
        }

        public IList<Data.Oficios> Oficios { get; set; }
        public IList<Data.TipoOficio> Tipos { get; set; }
        public IList<Data.Usuarios> Receptores { get; set; }

        public short GetRe(string nom, int table)
        {
            //if (table == 1) //receptores
            //{
            //    var w = _context.Receptores.Where(r => r.Nombre.ToUpper() == nom.ToUpper());
            //    if (w.Any())
            //    {
            //        return w.First().Id;
            //    }
            //    else
            //    {
            //        var r = new Data.Receptores();
            //        r.Nombre = nom.ToUpper();
            //        r.Oculto = false;
            //        r.Departamento = 1;
            //        r = _context.Receptores.Add(r).Entity;
            //        _context.SaveChanges();
            //        return r.Id;
            //    }
            //}
            if (table == 2)
            {
                var w = _context.Emisores.Where(r => r.Nombre.ToUpper().Trim() == nom.ToUpper().Trim());
                if (w.Any())
                {
                    return w.First().Id;
                }
                else
                {
                    var r = new Data.Emisores();
                    r.IdTipoEmisor = 10;
                    r.Nombre = nom.ToUpper().Trim();
                    r = _context.Emisores.Add(r).Entity;
                    _context.SaveChanges();
                    return r.Id;

                }
            }
            else if (table == 3)
            {
                var w = _context.TipoOficio.Where(r => r.Nombre.ToUpper().Trim() == nom.ToUpper().Trim());
                if (w.Any())
                {
                    return w.First().Id;
                }
                else
                {
                    var r = new Data.TipoOficio();
                    r.Nombre = nom.ToUpper().Trim();
                    r = _context.TipoOficio.Add(r).Entity;
                    _context.SaveChanges();
                    return r.Id;
                }
            }
            else if (table == 4)
            {
                var w = _context.Usuarios.Where(r => r.Nombre.ToUpper().Trim() == nom.ToUpper().Trim());
                if (w.Any())
                {
                    return (short)w.First().Id;
                }
                else
                {
                    if (nom.Contains("Mayela"))
                    {
                        return 10;
                    }
                    if (nom.Contains("Avila"))
                    {
                        return 4;
                    }
                    var r = new Data.Usuarios();
                    //r.Nombre = nom.ToUpper().Trim();
                    //r = _context.TipoOficio.Add(r).Entity;
                    //_context.SaveChanges();
                    return (short)r.Id;
                }
            }
            return 0;
        }

        public async Task OnGetAsync(int? id)
        {

            Oficios = await _context.Oficios.Where(o => o.Archivado == false).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
            Tipos = await _context.TipoOficio.ToListAsync();
            Receptores = await _context.Usuarios.Where(r => !r.Oculto && r.IdDepartamento == 1).ToListAsync();
        }
    }
}
