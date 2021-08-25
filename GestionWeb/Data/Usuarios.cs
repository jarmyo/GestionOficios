#nullable disable

namespace GestionWeb.Data
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            OficiosEstados = new HashSet<OficiosEstados>();
            OficiosIdReceptorNavigation = new HashSet<Oficios>();
            OficiosIdUsuarioNavigation = new HashSet<Oficios>();
            OficiosUsuarios = new HashSet<OficiosUsuarios>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string AspNetId { get; set; }
        public short IdDepartamento { get; set; }
        public bool Oculto { get; set; }
        public string Email { get; set; }

        public virtual Departamentos IdDepartamentoNavigation { get; set; }
        public virtual ICollection<OficiosEstados> OficiosEstados { get; set; }
        public virtual ICollection<Oficios> OficiosIdReceptorNavigation { get; set; }
        public virtual ICollection<Oficios> OficiosIdUsuarioNavigation { get; set; }
        public virtual ICollection<OficiosUsuarios> OficiosUsuarios { get; set; }
    }
}
