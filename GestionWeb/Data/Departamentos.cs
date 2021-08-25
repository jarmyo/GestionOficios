#nullable disable

namespace GestionWeb.Data
{
    public partial class Departamentos
    {
        public Departamentos()
        {
            Archiveros = new HashSet<Archiveros>();
            Oficios = new HashSet<Oficios>();
            Usuarios = new HashSet<Usuarios>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Archiveros> Archiveros { get; set; }
        public virtual ICollection<Oficios> Oficios { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
