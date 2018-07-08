using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;

namespace ProgramacionWeb3TP.Controllers {
    public class CarpetasController : Controller {

        // Service
        private CarpetaService _carpetaService = new CarpetaService();
        private UsuarioService _usuarioService = new UsuarioService();

        //TODO:Fake
        //private readonly int fakeUserId = 8;
        private int userIdInSession;

        // GET: Carpeta
        //Estando logueados --> Listado de carpetas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult Index(){
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }
            else {
                /*if (Request.Cookies["CookieUsuario"] != null) {
                    userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
                }
                else {
                    userIdInSession = (int)Session["usuarioSesionId"];
                }*/
                userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);
                System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userIdInSession);
            }
            List<Carpeta> carpetas = _carpetaService.ObtenerCarpetasPorUsuario(userIdInSession);
            if (!carpetas.Any()) {
                System.Diagnostics.Debug.WriteLine("Lista Carpetas Vacia");
            }
            else {
                System.Diagnostics.Debug.WriteLine("Lista Carpetas No Vacia");
            }
            return View(carpetas);
        }

        //Vista
        public ActionResult Crear()
        {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        //Procesar creación de carpeta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreacionCarpeta(Carpeta carpeta)
        {
            /*if (Request.Cookies["CookieUsuario"] != null) {
                userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            }
            else {
                userIdInSession = (int)Session["usuarioSesionId"];
            }*/
            userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);
            Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession); 
            carpeta.IdUsuario = userIdInSession;

            //System.Diagnostics.Debug.WriteLine("Home - Crear Carpeta: " + userIdInSession);

            System.Diagnostics.Debug.WriteLine("Crear carpeta - Usuario: " + usuarioActual.IdUsuario + " " + usuarioActual.Nombre);
            System.Diagnostics.Debug.WriteLine("Crear carpeta: " + carpeta.IdUsuario + " " + carpeta.Nombre + " " + carpeta.Descripcion);

            _carpetaService.CrearCarpeta(carpeta, usuarioActual);

            return RedirectToAction("Index");
        }
        
        //Vista
        public ActionResult Detalle(int? idCarpeta)
        {
            if (idCarpeta == null) {
                return RedirectToAction("Login", "Home");
            }
            else {
                if (Session["usuarioSesionId"] == null) {
                    String userNameInSession;
                    userNameInSession = "No user in session";
                    System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userNameInSession);
                    Session["returnPath"] = Request.RawUrl;
                    return RedirectToAction("Login", "Home");
                }
            }

            return RedirectToAction("Listado", "Tareas", new { idCarpeta });
        }

        //Vista
        public ActionResult Eliminar(int? idCarpeta)
        {
           
            if (idCarpeta == null) {
                return RedirectToAction("Login", "Home");
            }else {
                if (Session["usuarioSesionId"] == null) {
                    String userNameInSession;
                    userNameInSession = "No user in session";
                    System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userNameInSession);
                    Session["returnPath"] = Request.RawUrl;
                    return RedirectToAction("Login", "Home");
                }
            }

            Carpeta carpeta = _carpetaService.ObtenerCarpetaPorId(idCarpeta);
            return View(carpeta);
        }

        //Procesar eliminación de carpeta
        public ActionResult EliminacionCarpeta(Carpeta carpeta)
        {
            _carpetaService.EliminarCarpeta(carpeta.IdCarpeta);
            return RedirectToAction("Index");
        }

        //Vista
        public ActionResult Editar(int? idCarpeta)
        {
            if (idCarpeta == null) {
                return RedirectToAction("Login", "Home");
            }
            else {
                if (Session["usuarioSesionId"] == null) {
                    String userNameInSession;
                    userNameInSession = "No user in session";
                    System.Diagnostics.Debug.WriteLine("Home - Carpetas: " + userNameInSession);
                    Session["returnPath"] = Request.RawUrl;
                    return RedirectToAction("Login", "Home");
                }
            }

            Carpeta carpeta = _carpetaService.ObtenerCarpetaPorId(idCarpeta);
            return View(carpeta);
        }

        //Procesar Editar Carpeta
        public ActionResult EdicionCarpeta(Carpeta carpeta)
        {
            _carpetaService.EditarCarpeta(carpeta);
            return RedirectToAction("Index");
        }
    }
}