using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;



namespace ProgramacionWeb3TP.Servicios
{

    public class sUsuario
    {

        ContextoPractico ctx = new ContextoPractico();

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