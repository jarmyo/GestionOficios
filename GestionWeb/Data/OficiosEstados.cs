using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosEstados
    {
        public OficiosEstados()
        {
            OficiosEstadosNotas = new HashSet<OficiosEstadosNotas>();
        }

        public int Id { get; set; }
        public int IdOficio { get; set; }
        public DateTime FechaHora { get; set; }
        public byte IdEstado { get; set; }
        public int IdUsuario { get; set; }

        public virtual Estados IdEstadoNavigation { get; set; }
        public virtual Oficios IdOficioNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<OficiosEstadosNotas> OficiosEstadosNotas { get; set; }
    }
}
