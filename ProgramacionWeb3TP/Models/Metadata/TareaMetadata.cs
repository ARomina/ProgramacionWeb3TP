using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class TareaMetadata
    {
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Estimado en Horas")]
        public Nullable<decimal> EstimadoHoras { get; set; }

        [Display(Name = "Fecha Fin")]
        public Nullable<System.DateTime> FechaFin { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime FechaCreacion { get; set; }
    }
}