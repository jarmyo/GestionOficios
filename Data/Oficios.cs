using System;
using System.Collections.Generic;

#nullable disable

namespace GestionWeb.Data
{
    public partial class Oficios
    {
        public Oficios()
        {
            OficiosDocumentos = new HashSet<OficiosDocumentos>();
            OficiosEstados = new HashSet<OficiosEstados>();
            OficiosUsuarios = new HashSet<OficiosUsuarios>();
        }

        public int Id { get; set; }
        public string Numero { get; set; }
        public string Oficio { get; set; }
        public short IdEmisor { get; set; }
        public short IdDepartamento { get; set; }
        public short IdTipo { get; set; }
        public string Asunto { get; set; }
        public bool Archivado { get; set; }
        public int IdReceptor { get; set; }
        public DateTime FechaRecepcion { get; set; }

        public virtual Departamentos IdDepartamentoNavigation { get; set; }
        public virtual Emisores IdEmisorNavigation { get; set; }
        public virtual Usuarios IdReceptorNavigation { get; set; }
        public virtual TipoOficio IdTipoNavigation { get; set; }
        public virtual OficiosArchivado OficiosArchivado { get; set; }
        public virtual OficiosTermino OficiosTermino { get; set; }
        public virtual ICollection<OficiosDocumentos> OficiosDocumentos { get; set; }
        public virtual ICollection<OficiosEstados> OficiosEstados { get; set; }
        public virtual ICollection<OficiosUsuarios> OficiosUsuarios { get; set; }
    }
}
