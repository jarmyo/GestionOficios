#nullable disable

namespace GestionWeb.Data
{
    public partial class TiposDeEmisor
    {
        public TiposDeEmisor()
        {
            Emisores = new HashSet<Emisores>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Emisores> Emisores { get; set; }
    }
}
