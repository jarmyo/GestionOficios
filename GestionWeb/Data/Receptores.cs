using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class Receptores
    {
        public Receptores()
        {
            Oficios = new HashSet<Oficios>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public bool Oculto { get; set; }
        public short Departamento { get; set; }

        public virtual ICollection<Oficios> Oficios { get; set; }
    }
}
