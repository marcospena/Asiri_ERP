﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MVC_BusinessEntity;

namespace Asiri_ERP.Models
{
    using System;
    using System.Collections.Generic;

    [Bind(Exclude ="idPersona")]
    [MetadataType(typeof(RHUt09_persona))]
    public partial class RHUt09_persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt09_persona()
        {
            this.RHUt01_empleado = new HashSet<RHUt01_empleado>();
            this.RHUt07_paciente = new HashSet<RHUt07_paciente>();
            this.RHUt10_personaRedSocial = new HashSet<RHUt10_personaRedSocial>();
        }

        //public long idPersona { get; set; }
        [Required(ErrorMessage="Introduce el nombre de la persona")]
        public string nombrePersona { get; set; }
        [Required]
        public string apellidoPaterno { get; set; }
        [Required]
        public string apellidoMaterno { get; set; }
        public string numDocIdentidad { get; set; }
        public string razonSocial { get; set; }
        public Nullable<System.DateTime> fecNacimiento { get; set; }
        public string nombreVia { get; set; }
        public string numVia { get; set; }
        public string nombreZona { get; set; }
        public string direccion01 { get; set; }
        public string direccion02 { get; set; }
        public string numTelefonico01 { get; set; }
        public string numTelefonico02 { get; set; }
        public string email01 { get; set; }
        public string email02 { get; set; }
        public string sexo { get; set; }
        public bool difunto { get; set; }
        public Nullable<System.DateTime> fecDefuncion { get; set; }
        public string pathFoto { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public long idUsuario { get; set; }
        public Nullable<long> idUsuarioModificar { get; set; }
        public Nullable<long> idUsuarioEliminar { get; set; }
        public int idVia { get; set; }
        public int idZona { get; set; }
        public int idTipoDocIdentidad { get; set; }
        public int idDistrito { get; set; }
        public short idEstadoCivil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt01_empleado> RHUt01_empleado { get; set; }
        public virtual RHUt05_estadoCivil RHUt05_estadoCivil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt07_paciente> RHUt07_paciente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt10_personaRedSocial> RHUt10_personaRedSocial { get; set; }
        public virtual RHUt12_tipoDocIdentidad RHUt12_tipoDocIdentidad { get; set; }
        public virtual UBIt01_distrito UBIt01_distrito { get; set; }
        public virtual UBIt04_via UBIt04_via { get; set; }
        public virtual UBIt05_zona UBIt05_zona { get; set; }
    }
}
