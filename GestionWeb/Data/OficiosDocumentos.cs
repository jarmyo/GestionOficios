#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosDocumentos
    {
        public int Id { get; set; }
        public int IdOficio { get; set; }
        public string filename { get; set; }

        public virtual Oficios IdOficioNavigation { get; set; }
    }
}
