#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosArchivado
    {
        public int Id { get; set; }
        public short IdCajon { get; set; }

        public virtual Cajon IdCajonNavigation { get; set; }
        public virtual Oficios IdNavigation { get; set; }
    }
}
