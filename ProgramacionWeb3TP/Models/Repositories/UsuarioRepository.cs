using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramacionWeb3TP.Models.Repositories
{
    public class UsuarioRepository {

        public static List<Usuario> listUsuario = new List<Usuario>();

        public Usuario registrarUsuario(Usuario usuario) {
            Usuario usuarioNuevo = new Usuario(usuario);
            listUsuario.Add(usuarioNuevo);
            return usuarioNuevo;
        }


        /*Obtener Usuario por Id*/
        public Usuario ObtenerId(int IdUsuario)
        {
            foreach(var usuid in listUsuario)
            {
                {
                    return usuId;
                }
                //if (usuid.idUsuario = IdUsuario)

            }
            return null;
        }

        
        /*Obtenemos usuario por Email*/
        public Usuario obtenerEmail(string Email)
        {
            foreach (var usu in listUsuario)
            { 

                if (usu.email.equals(Email))
                {
                    return Usu;
                }
            }
            return null;
        }


        /*Harcodeo usuario N para verificar validación del Login */
        public List<Usuario> cargaListado() {

            Usuario U1 = new Usuario();
            U1.IdUsuario = 1;
            U1.nombre = "Rama";
            U1.Apellido = "Silva";
            U1.Mail = "rama@rama.com";
            U1.contrasenia = "123";

            Usuario U2 = new Usuario();
            U2.IdUsuario = 2;
            U2.nombre = "Luis";
            U2.apellido = "Gonzalez";
            U2.Mail = "luis@luis.com";
            U2.contrasenia = "luis";

           listUsuario.add(U1);
           listUsuario.add(U2);

            return listUsuario();
        }

        public UsuarioRepository validarUsuario(Usuario usuario) {

  

        }
    }
}