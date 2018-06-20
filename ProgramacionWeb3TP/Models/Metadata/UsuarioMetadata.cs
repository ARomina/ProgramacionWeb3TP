using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Metadata
{
    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Debe tener como máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Debe tener como máximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        [StringLength(20, ErrorMessage = "Debe tener como máximo 20 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Debe tener como máximo 20 caracteres")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a - z])(?=.*[A - Z])(?=.*\d).+$", ErrorMessage = "Debe contener al menos 1 mayúscula, 1 minúscula y 1 número")]
        public string Contrasenia { get; set; }

        //PROBABLEMENTE ESTO NO VAYA
        /*[Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Debe tener como máximo 20 caracteres")]
        [DataType(DataType.Password)]
        [CompareAttribute("Contrasenia", ErrorMessage = "Las contraseñas no coinciden")]
        public string Contrasenia2 { get; set; }*/

        public short Activo { get; set; }
        public System.DateTime FechaRegistracion { get; set; }
        public Nullable<System.DateTime> FechaActivacion { get; set; }
        public string CodigoActivacion { get; set; }

        public Usuario(Usuario usuario) {
            this.Nombre = usuario.Nombre;
            this.Apellido = usuario.Apellido;
            this.Email = usuario.Email;
            this.Contrasenia = usuario.Contrasenia;
            this.Activo = usuario.Activo;
            this.FechaRegistracion = DateTime.ParseExact((DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
            this.FechaActivacion = usuario.FechaActivacion;
            this.CodigoActivacion = Guid.NewGuid().ToString("D").ToUpper().Substring(0, 30);
        }

        //Constructor que uso para registrar al nuevo usuario dentro del repository
        public Usuario(Usuario usuario, short activo, Nullable<System.DateTime> fechaActivacion) {
            this.Nombre = usuario.Nombre;
            this.Apellido = usuario.Apellido;
            this.Email = usuario.Email;
            this.Contrasenia = usuario.Contrasenia;
            this.Activo = activo;
            this.FechaRegistracion = DateTime.Now;
            this.FechaActivacion = fechaActivacion;
            this.CodigoActivacion = Guid.NewGuid().ToString("D").ToUpper().Substring(0, 30);
        }

        //Constructor que uso para mandar los datos del registro al repository
        public Usuario(string nombre, string apellido, string email, string contrasenia) {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Contrasenia = contrasenia;
        }

        public Usuario(string nombre, string apellido, string email, string contrasenia,
                        short activo, System.DateTime fechaRegistracion, 
                        Nullable<System.DateTime> fechaActivacion, string codigoActivacion) {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Contrasenia = contrasenia;
            this.Activo = activo;
            this.FechaRegistracion = fechaRegistracion;
            this.FechaActivacion = fechaActivacion;
            this.CodigoActivacion = codigoActivacion;
        }
    }
}