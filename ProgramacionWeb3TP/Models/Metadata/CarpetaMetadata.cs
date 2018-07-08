using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class CarpetaMetadata {

        [Required(ErrorMessage = "La carpeta debe tener un nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime FechaCreacion { get; set; }
    }
}