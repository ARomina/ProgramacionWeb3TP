using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Entities {
    public class Carpeta {
        public int IdCarpeta { get; set; }
        public int IdUsuario { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Setear los otros parametros --> id desde la base, datetime desde pc
        public Carpeta(int idUsuario, String nombre, String descripcion) {
            this.IdUsuario = idUsuario;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }
    }
}