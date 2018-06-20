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


}