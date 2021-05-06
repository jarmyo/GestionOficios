using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public bool MostrarArchivados { get; set; }

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
                    return (short) w.First().Id;
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
                    return (short) r.Id;
                }
            }
            return 0;
        }


            public async Task OnGetAsync(int? id)
            {


            //  var FILE = System.IO.File.ReadAllLines("c:\\EXTRA.csv");

            //foreach (var linea in FILE)
            //{
            //    var campos = linea.Split(',');

            //    //if (campos[1]=="99")
            //    //{

            //    //}
            //    //else
            //    //{
            //    //    var o = new Data.OficiosTermino();
            //    //    o.Id = Convert.ToInt32(campos[0]);
            //    //    var f = _context.Oficios.First(o1 => o1.Id == o.Id).FechaRecepcion;
            //    //    o.Fecha =  f.AddDays(Convert.ToInt32(campos[1]));
            //    //    _context.OficiosTermino.Add(o);
            //    //    _context.SaveChanges();
            //    //}


            //    if (campos[2] != "NO")
            //    {
            //        var o = new Data.OficiosUsuarios();
            //        o.IdOficio = Convert.ToInt32(campos[0]);
            //        o.IdUsuario = GetRe(campos[2], 4);
            //        _context.OficiosUsuarios.Add(o);
            //        _context.SaveChanges();
            //    }

            //    if (campos[3] != "NO")
            //    {
            //        var o = new Data.OficiosUsuarios();
            //        o.IdOficio = Convert.ToInt32(campos[0]);
            //        o.IdUsuario = GetRe(campos[3], 4);
            //        _context.OficiosUsuarios.Add(o);
            //        _context.SaveChanges();
            //    }


            //    //        var o = new Data.Oficios();
            //    //        o.Numero = campos[1];
            //    //        o.Oficio = campos[2];
            //    //        o.IdReceptor = GetRe(campos[3], 1);
            //    //        o.IdEmisor = GetRe(campos[4], 2);
            //    //        o.IdDepartamento = 1;
            //    //        o.IdTipo = GetRe(campos[6], 3);
            //    //        o.Asunto = campos[7];
            //    //        o.Archivado = campos[8] == "1";
            //    //        o.FechaRecepcion = DateTime.Parse(campos[9]);

            //    //        _context.Oficios.Add(o);
            //    _context.SaveChanges();

            //}


            ////id servira para checar si es 
            //foreach (var o in _context.Oficios.ToList())
            //{
                //if (!o.OficiosEstados.Any())
                //{
                //    var F = new Data.OficiosEstados();
                //    F.IdOficio = o.Id;
                //    F.FechaHora = o.FechaRecepcion;
                //    F.IdUsuario = o.IdReceptor;
                //    F.IdEstado = 1;
                //    _context.OficiosEstados.Add(F);
                //    _context.SaveChanges();
                //}
                //
            //foreach (var o1 in _context.OficiosEstadosNotas.ToList())
            //{
            //    o1.FechaHora = o1.IdEstadoOficioNavigation.FechaHora;
            //}
            //_context.SaveChanges();

                //foreach (var u in o.OficiosUsuarios.ToList())
                //{
                //    if (o.OficiosEstados.Count(s => s.IdEstado == 3)> 1)
                //    {

                //        _context.OficiosEstados.Remove(o.OficiosEstados.First(s => s.IdEstado == 3));
                //        _context.SaveChanges();

                //        //var F = new Data.OficiosEstados();
                //        //F.IdOficio = o.Id;
                //        //F.FechaHora = o.FechaRecepcion;
                //        //F.IdUsuario = u.IdUsuario;
                //        //F.IdEstado = 2;
                //        //F = _context.OficiosEstados.Add(F).Entity;
                //        //_context.SaveChanges();

                //        //_context.OficiosEstadosNotas.Add(new Data.OficiosEstadosNotas()
                //        //{
                //        //    FechaHora = DateTime.Now,
                //        //    IdEstadoOficio = F.Id,
                //        //    Nota = "Turnado desde recepción a " + _context.Usuarios.First(u1 => u1.Id == u.IdUsuario).Nombre
                //        //});

                //        //_context.SaveChanges();

                //        //var F2 = new Data.OficiosEstados();
                //        //F2.IdOficio = o.Id;
                //        //F2.FechaHora = o.FechaRecepcion;
                //        //F2.IdUsuario = u.IdUsuario;
                //        //F2.IdEstado = 3;
                //        //_context.OficiosEstados.Add(F2);
                //        //_context.SaveChanges();
                //    }
                //}

          //  }



            MostrarArchivados = id != null;

             //   await _context.SaveChangesAsync();
                Oficios = await _context.Oficios.Where(o => o.Archivado == MostrarArchivados).OrderByDescending(o => o.FechaRecepcion).ToListAsync();
                Tipos = await _context.TipoOficio.ToListAsync();
                Receptores = await _context.Usuarios.Where(r => !r.Oculto && r.IdDepartamento == 1).ToListAsync();


            }
        }
    }
