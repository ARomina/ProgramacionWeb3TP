using ProgramacionWeb3TP.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models
{
    [MetadataType(typeof(TareaMetadata))]
    public partial class Tarea
    {
        //Constructor para enviar al servicio de crear tarea
        public Tarea(int idCarpeta) {
            this.IdCarpeta = idCarpeta;
        }

            //Constructor para enviar al servicio de crear tarea
            public Tarea(Tarea tarea) {
            this.IdCarpeta = tarea.IdCarpeta;
            this.IdUsuario = tarea.IdUsuario;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Prioridad = 1;
            this.Completada = 0;
            this.FechaCreacion = DateTime.Now;
        }
    }
}