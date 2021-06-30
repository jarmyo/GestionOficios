using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosEstadosNotas
    {
        public int Id { get; set; }
        public int IdEstadoOficio { get; set; }
        public string Nota { get; set; }
        public DateTime FechaHora { get; set; }

        public virtual OficiosEstados IdEstadoOficioNavigation { get; set; }
    }
}
