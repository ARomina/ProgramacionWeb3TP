using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Entities {
    public class ComentarioTarea {
        public int IdComentarioTarea { get; set; }
        public String Texto { get; set; }
        public int IdTarea { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Setear los otros parametros --> id desde la base, datetime desde pc
        public ComentarioTarea(String texto, int idTarea) {
            this.Texto = texto;
            this.IdTarea = idTarea;
        }
    }
}