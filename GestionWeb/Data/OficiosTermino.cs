#nullable disable

namespace GestionWeb.Data
{
    public partial class OficiosTermino
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Oficios IdNavigation { get; set; }
    }
}
