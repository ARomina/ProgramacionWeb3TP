using ProgramacionWeb3TP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Repositories
{
    public class UsuarioRepository
    {
        public static List<Usuario> listUsuario = new List<Usuario>();

        public Usuario registrarUsuario(Usuario usuario)
        {
            Usuario usuarioNuevo = new Usuario(usuario);
            listUsuario.Add(usuarioNuevo);
            return usuarioNuevo;
        }
    }
}