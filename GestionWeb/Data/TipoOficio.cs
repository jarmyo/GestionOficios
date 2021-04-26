using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class TipoOficio
    {
        public TipoOficio()
        {
            Oficios = new HashSet<Oficios>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Oficios> Oficios { get; set; }
    }
}
