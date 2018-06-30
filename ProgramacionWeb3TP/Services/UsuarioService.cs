using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Services {
    public class UsuarioService {
        private TaskieContext ctx = new TaskieContext();
        private CarpetaService _carpetaService = new CarpetaService();

        //Variables estaticas para asignar si estan activos o no
        private static short USUARIO_NO_ACTIVO = 0;
        private static short USUARIO_ACTIVO = 1;

        //Variables para mostrar los mensajes de error
        private static string MENSAJE_MAIL_PASS_INCORRECTOS = "E-mail y/o contraseña incorrectos, por favor intentá nuevamente";
        private static string MENSAJE_MAIL_EXISTENTE = "Ese e-mail no está disponible, por favor ingresá otro";

        //Codigo de error a asignar - Registro
        private int codigoErrorRegistro = 0;

        public Usuario ObtenerUsuarioPorId(int id) {

            var user = (from u in ctx.Usuario
                        where u.IdUsuario == id
                        select u)
                        .FirstOrDefault();

            return user;
        }

        public Usuario loguearUsuarioPorEmail(Usuario usuario) {
            Usuario usuarioOK = new Usuario();

            usuarioOK = ctx.Usuario.Where(u => u.Email == usuario.Email
                                               && u.Contrasenia == usuario.Contrasenia).SingleOrDefault();


            return usuarioOK;
        }


        public Usuario buscarUsuarioPorEmail(string usuario) {
            Usuario usuarioEncontrado = new Usuario();

            usuarioEncontrado = ctx.Usuario.Where(u => u.Email == usuario).SingleOrDefault();

            return usuarioEncontrado;
        }

        public Usuario registrarUsuario(Usuario usuario, string pass2) {
            Usuario usuarioNuevo = null;
            if (chequearSiMailsCoinciden(usuario.Contrasenia, pass2)) {
                usuarioNuevo = new Usuario(usuario, USUARIO_NO_ACTIVO, null);

                //Para chequear los datos
                System.Diagnostics.Debug.WriteLine("Datos del usuario nuevo a crear: " + usuarioNuevo.Nombre + " " + usuarioNuevo.Apellido + " "
                                                             + usuarioNuevo.Email + " " + usuarioNuevo.Contrasenia + " " + usuarioNuevo.Activo + " " + usuarioNuevo.FechaRegistracion
                                                             + " " + usuarioNuevo.FechaActivacion + " " + usuarioNuevo.CodigoActivacion);

                if (chequearSiExisteEmail(usuarioNuevo.Email)) {
                    if (chequearSiEstaActivo(usuarioNuevo.Email)) {
                        //Para avisar que ya existe un usuario activo con ese mail
                        codigoErrorRegistro = 1;
                        usuarioNuevo = null;
                    }
                    else {
                        activarRegistroUsuarioExistente(usuarioNuevo);
                        Usuario usuarioActivado = buscarUsuarioPorEmail(usuarioNuevo.Email);
                        _carpetaService.crearCarpetaGeneral(usuarioActivado.IdUsuario);
                        usuarioNuevo = usuarioActivado;
                    }
                }
                else {
                    try {
                        ctx.Usuario.Add(usuarioNuevo);
                        ctx.SaveChanges();
                    }
                    catch (DbEntityValidationException ex) {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                            foreach (var validationError in entityValidationErrors.ValidationErrors) {
                                System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            else {
                //Para mostrar algo si no coinciden los emails
                codigoErrorRegistro = 0;
            }

            return usuarioNuevo;
        }

        public bool chequearSiMailsCoinciden(string pass1, string pass2) {
            if (pass2.Equals(pass1)) {
                return true;
            }
            else {
                return false;
            }
        }

        public bool chequearSiExisteEmail(string email) {
            //var usuario = context.Usuarios.Where(u => u.Email == email).Select(u1 => u1).First();
            Usuario usuario = (from u in ctx.Usuario
                               where u.Email == email
                               select u).FirstOrDefault();
            if (usuario != null) {
                return true;
            }
            else {
                return false;
            }
        }

        public bool chequearSiEstaActivo(string email) {
            //var usuario = context.Usuarios.Where(u => u.Email == email && u.Activo == 1).Select(u1 => u1);
            Usuario usuario = (from u in ctx.Usuario
                               where u.Email == email && u.Activo == 1
                               select u).FirstOrDefault();
            if (usuario != null) {
                return true;
            }
            else {
                return false;
            }
        }

        public void activarRegistroUsuarioExistente(Usuario usuario) {
            //var usuarioExistente = context.Usuarios.Where(u => u.IdUsuario == usuario.IdUsuario).First();
            Usuario usuarioExistente = (from u in ctx.Usuario
                                        where u.Email == usuario.Email
                                        select u).First();
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Contrasenia = usuario.Contrasenia;
            usuarioExistente.Activo = USUARIO_ACTIVO;
            usuarioExistente.FechaActivacion = DateTime.Now;
            ctx.SaveChanges();

            //return usuarioExistente;
        }

        public String mostrarMensajeDeError() {
            string mensaje = "";
            if (codigoErrorRegistro == 0) {
                mensaje = MENSAJE_MAIL_PASS_INCORRECTOS;
            }
            else {
                mensaje = MENSAJE_MAIL_EXISTENTE;
            }
            return mensaje;
        }

    }
}