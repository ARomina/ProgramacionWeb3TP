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

        //Login
        public ActionResult Login() {
            return View();
        }

        //Registro
        public ActionResult Registracion() {
            return View();
        }

        //Registrar Usuario
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario) {
            if (usuarioRepository.registrarUsuario(usuario) != null) {
                return RedirectToAction("Home", "Usuario");
            }else {
                return RedirectToAction("Registracion", "Home");
            } 
        }
    }
}