using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class Archiveros
    {
        public Archiveros()
        {
            Cajon = new HashSet<Cajon>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public short IdDepartamento { get; set; }

        public virtual Departamentos IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Cajon> Cajon { get; set; }
    }
}
