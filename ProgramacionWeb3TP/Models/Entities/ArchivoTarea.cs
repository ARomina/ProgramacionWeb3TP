using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Entities {
    public class ArchivoTarea {
        public int IdArchivoTarea { get; set; }
        public String RutaArchivo { get; set; }
        public int IdTarea { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Setear los otros parametros --> id desde la base, datetime desde pc
        public ArchivoTarea(String rutaArchivo, int idTarea) {
            this.RutaArchivo = rutaArchivo;
            this.IdTarea = idTarea;
        }
    }
}