﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using GestionWeb.Data;

//namespace GestionWeb.Areas.Oficios.Pages.fold
//{
//    public class IndexModel : PageModel
//    {
//        private readonly GestionWeb.Data.GestionOficiosContext _context;

//        public IndexModel(GestionWeb.Data.GestionOficiosContext context)
//        {
//            _context = context;
//        }

//        public IList<Oficios> Oficios { get;set; }

//        public async Task OnGetAsync()
//        {
//            Oficios = await _context.Oficios
//                .Include(o => o.IdDepartamentoNavigation)
//                .Include(o => o.IdEmisorNavigation)
//                .Include(o => o.IdReceptorNavigation)
//                .Include(o => o.IdTipoNavigation).ToListAsync();
//        }
//    }
//}
