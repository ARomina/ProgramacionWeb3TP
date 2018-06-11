using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Entities {
    public class Tarea {
        public int IdTarea { get; set; }
        public int IdCarpeta { get; set; }
        public int IdUsuario { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public Decimal EstimadoHoras { get; set; }
        public DateTime FechaFin { get; set; }
        public Int16 Prioridad { get; set; }
        public Int16 Completada { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Setear los otros parametros --> id desde la base, datetime desde pc
        public Tarea(int idCarpeta, int idUsuario, String nombre, String descripcion, 
            Decimal estimadoHoras, DateTime fechaFin, Int16 prioridad, Int16 completada) {
            this.IdCarpeta = idCarpeta;
            this.IdUsuario = idUsuario;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.EstimadoHoras = estimadoHoras;
            this.FechaFin = fechaFin;
            this.Prioridad = prioridad;
            this.Completada = completada;
        }
    }
}