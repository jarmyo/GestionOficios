using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Oficios = new HashSet<Oficios>();
            OficiosEstados = new HashSet<OficiosEstados>();
            OficiosUsuarios = new HashSet<OficiosUsuarios>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string AspNetId { get; set; }
        public short IdDepartamento { get; set; }
        public bool Oculto { get; set; }
        public string Email { get; set; }

        public virtual Departamentos IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Oficios> Oficios { get; set; }
        public virtual ICollection<OficiosEstados> OficiosEstados { get; set; }
        public virtual ICollection<OficiosUsuarios> OficiosUsuarios { get; set; }
    }
}
