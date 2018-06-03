using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class UsuarioController : Controller {
        
        // GET: Usuario
        public ActionResult HomeUsuario() {
            return View();
        }

        //Vista
        public ActionResult Modificar() {
            return View();
        }

        //Procesar modificacion de perfil de usuario
        public ActionResult ModificacionUsuario() {
            return RedirectToAction("HomeUsuario", "Usuario");
        }
    }
}