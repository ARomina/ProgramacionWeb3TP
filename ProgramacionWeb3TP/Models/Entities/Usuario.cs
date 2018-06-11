using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models {
    public class Usuario {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public Int16 Activo { get; set; }
        public DateTime FechaRegistracion { get; set; }
        public DateTime FechaActivacion { get; set; }
        public string CodigoActivacion { get; set; }

        public Usuario() {
        }

        public Usuario(Usuario usuario) {
            //Agregar Id después
            this.Nombre = usuario.Nombre;
            this.Apellido = usuario.Apellido;
            this.Email = usuario.Email;
            this.Contrasenia = usuario.Contrasenia;
            //Agregar Activo después
            this.FechaRegistracion = DateTime.Now;  //Cambiar después
            this.FechaActivacion = DateTime.Now;    //Cambiar después
            this.CodigoActivacion = "AAAZZZ";  //Cambiar después
        }

    }
}
