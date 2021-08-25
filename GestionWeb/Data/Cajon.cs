#nullable disable

namespace GestionWeb.Data
{
    public partial class Cajon
    {
        public Cajon()
        {
            OficiosArchivado = new HashSet<OficiosArchivado>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; }
        public short IdArchivero { get; set; }

        public virtual Archiveros IdArchiveroNavigation { get; set; }
        public virtual ICollection<OficiosArchivado> OficiosArchivado { get; set; }
    }
}
