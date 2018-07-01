using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProgramacionWeb3TP.Models.Metadata;
using System.Globalization;

namespace ProgramacionWeb3TP.Models
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario {
        public string NombreCompleto {
            get {
                return this.Apellido + ", " + this.Nombre;
            }
            set { }
        }

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

        //public List<Carpeta> listaCarpetas  { get; set; }
    }
}