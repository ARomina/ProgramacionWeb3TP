using ProgramacionWeb3TP.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models
{
    [MetadataType(typeof(CarpetaMetadata))]
    public partial class Carpeta
    {
        //Constructor para crear nueva carpeta
        public Carpeta() {
        }

        //Constructor de carpeta General al activar un usuario
        public Carpeta(int idUsuario) {
            this.IdUsuario = idUsuario;
            this.Nombre = "General";
            this.Descripcion = null;
            this.FechaCreacion = DateTime.Now;
        }

        //Constructor para enviar al servicio de crear carpeta
        public Carpeta(Carpeta carpeta) {
            this.IdUsuario = carpeta.IdUsuario;
            this.Nombre = carpeta.Nombre;
            this.Descripcion = carpeta.Descripcion;
            this.FechaCreacion = DateTime.Now;
        }
    }
}