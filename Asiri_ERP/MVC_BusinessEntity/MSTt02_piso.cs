//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_BusinessEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class MSTt02_piso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MSTt02_piso()
        {
            this.CLlt07_consultorio = new HashSet<CLlt07_consultorio>();
        }
    
        public short idPiso { get; set; }
        public short numPiso { get; set; }
        public string obsvPiso { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public long idUsuario { get; set; }
        public Nullable<long> idUsuarioModificar { get; set; }
        public Nullable<long> idUsuarioEliminar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt07_consultorio> CLlt07_consultorio { get; set; }
    }
}
