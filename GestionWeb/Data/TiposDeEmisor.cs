using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class TiposDeEmisor
    {
        public TiposDeEmisor()
        {
            Emisores = new HashSet<Emisores>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Emisores> Emisores { get; set; }
    }
}
