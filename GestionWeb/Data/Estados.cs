#nullable disable

namespace GestionWeb.Data
{
    public partial class Estados
    {
        public Estados()
        {
            OficiosEstados = new HashSet<OficiosEstados>();
        }

        public EstadoOficio Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public byte? DependeDe { get; set; }

        public virtual ICollection<OficiosEstados> OficiosEstados { get; set; }
    }
}
