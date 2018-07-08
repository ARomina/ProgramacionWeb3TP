using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class TareaMetadata {

        [Required(ErrorMessage = "La tarea debe tener un nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Display(Name = "Estimado en Horas")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Formato de horas incorrecto (hasta 2 decimales)")]
        public Nullable<decimal> EstimadoHoras { get; set; }

        [Display(Name = "Fecha Fin")]
        public Nullable<System.DateTime> FechaFin { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime FechaCreacion { get; set; }
    }
}