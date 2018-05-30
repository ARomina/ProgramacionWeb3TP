using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers{
    public class HomeController : Controller {
        public static List<Usuario> listUsuarios;
        private UsuarioRepository usuarioRepository = new UsuarioRepository();

        //Sin estar loggueados

        // GET: Home
        public ActionResult Index() {
            return View();
        }

        //Login dirige a página principal
        [HttpGet]
        public ActionResult Login() {

            Usuario usuario = new Usuario();

            return RedirectToAction("Login", "Home");
        }

        //Validar Login
        [HttpPost]
        public ActionResult ValidarLogin(string Email, string Contrasenia)
        {            
            if(usuarioRepository.obtenerEmail != null)
            {
                if(usuarioRepository.obtenerUsuario != null){

                }
            }

            return View();
                
        }

        //Registro
        public ActionResult Registracion() {
            return View();
        }

        //Registrar Usuario
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario) {
            Usuario usuarioNuevo = usuarioRepository.registrarUsuario(usuario);
            if (usuarioNuevo != null) {
                return RedirectToAction("Login", "Home");
            }else {
                return RedirectToAction("Registracion", "Home");
            }
            
        }
    }
}