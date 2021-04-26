using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class Emisores
    {
        public Emisores()
        {
            Oficios = new HashSet<Oficios>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public byte? IdTipoEmisor { get; set; }

        public virtual TiposDeEmisor IdTipoEmisorNavigation { get; set; }
        public virtual ICollection<Oficios> Oficios { get; set; }
    }
}
