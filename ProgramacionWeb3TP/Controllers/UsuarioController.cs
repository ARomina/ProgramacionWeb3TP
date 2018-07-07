using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class UsuarioController : Controller {
        private int userIdInSession;

        // GET: Usuario
        public ActionResult Index() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Usuario: " + userNameInSession);
            }
            else {
                System.Diagnostics.Debug.WriteLine("Home - Usuario: " + Session["usuarioSesionId"]);
                userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
                System.Diagnostics.Debug.WriteLine("Home - Usuario: " + userIdInSession);
            }
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
            Request.Cookies.Remove("CookieUsuario");
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}