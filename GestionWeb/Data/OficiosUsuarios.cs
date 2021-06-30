using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosUsuarios
    {
        public int Id { get; set; }
        public int IdOficio { get; set; }
        public int IdUsuario { get; set; }

        public virtual Oficios IdOficioNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
