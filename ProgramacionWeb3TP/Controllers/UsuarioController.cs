using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class UsuarioController : Controller {
        // GET: Usuario
        public ActionResult Index() {
            String userNameInSession;
            if (Session["usuarioEnSesion"] == null) {
                userNameInSession = "No user in session";
            }
            else {
                userNameInSession = (String)Session["usuarioEnSesion"];
            }
            System.Diagnostics.Debug.WriteLine("Usuario home: " + userNameInSession);
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

        //Procesar logout
        public ActionResult Logout() {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}