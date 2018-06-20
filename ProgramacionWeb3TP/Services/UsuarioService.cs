using ProgramacionWeb3TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Services
{
    public class UsuarioService
    {
        private TaskieContext ctx = new TaskieContext();

        public Usuario ObtenerUsuarioPorId(int id)
        {

            var user = (from u in ctx.Usuario
                        where u.IdUsuario == id
                        select u)
                        .FirstOrDefault();

            return user;
        }

        public Usuario loguearUsuarioPorEmail(Usuario usuario)
        {
            Usuario usuarioOK = new Usuario();

            usuarioOK = ctx.Usuario.Where(u => u.Email == usuario.Email
                                               && u.Contrasenia == usuario.Contrasenia).SingleOrDefault();

            return usuarioOK;
        }


        public Usuario buscarUsuarioPorEmail(string usuario)
        {
            Usuario usuarioEncontrado = new Usuario();

            usuarioEncontrado = ctx.Usuario.Where(u => u.Email == usuario).SingleOrDefault();

            return usuarioEncontrado;
        }
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
                            //Avisar que ya existe un usuario activo con ese mail
                        }else {
                            Usuario usuarioActivado =  activarRegistroUsuarioExistente(usuarioNuevo);
                            usuarioNuevo = usuarioActivado;
                        }
                    }else {
                        try {
                            ctx.Usuarios.Add(usuarioNuevo);
                            ctx.SaveChanges();
                        }catch (DbEntityValidationException ex) {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors) {
                                foreach (var validationError in entityValidationErrors.ValidationErrors) {
                                    System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
            }else {
                //Habria que mostrar algo si no coinciden los emails
            }
            return usuarioNuevo;
        }

        public bool chequearSiMailsCoinciden(string pass1, string pass2) {
            if (pass2.Equals(pass1)) {
                return true;
            }else {
                return false;
            }
        }

        public bool chequearSiExisteEmail(string email) {
            var usuario = ctx.Usuarios.Where(u => u.Email == email).Select(u1 => u1);
            if (usuario != null) {
                return true;
            }else {
                return false;
            }
        }

        public bool chequearSiEstaActivo(string email) {
            var usuario = ctx.Usuarios.Where(u => u.Email == email && u.Activo == 1).Select(u1 => u1);
            if (usuario != null) {
                return true;
            }else {
                return false;
            }
        }

        public Usuario activarRegistroUsuarioExistente(Usuario usuario) {
            var usuarioExistente = ctx.Usuarios.Where(u => u.IdUsuario == usuario.IdUsuario).First();
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Contrasenia = usuario.Contrasenia;
            usuarioExistente.Activo = USUARIO_ACTIVO;
            usuarioExistente.FechaActivacion = DateTime.Now;

            try {
                ctx.SaveChanges();
            } catch(Exception e){
                //No se pudo modificar la informacion
            }

            return usuarioExistente;
        }
    }

}