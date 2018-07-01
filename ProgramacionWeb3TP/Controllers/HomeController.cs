using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Net;


namespace ProgramacionWeb3TP.Controllers{


    public class HomeController : Controller {
        private UsuarioService _usuarioService = new UsuarioService();

        //Sin estar loggueados

        // GET: Home
        public ActionResult Index() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Index: " + userNameInSession);
            }
            else {
                int userIdInSession;
                userIdInSession = (int) Session["usuarioSesionId"];
                System.Diagnostics.Debug.WriteLine("Home: " + userIdInSession);
            }
            return View();
        }

        //Pantalla Login
        public ActionResult Login() {
            return View();
        }

        //Pantalla Registro
        public ActionResult Registracion() {
            return View();
        }

        //Proceso de logueo de usuario
        /*Validación del Usuario Ingresado*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerificarUsuario(Usuario usuario) {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Contrasenia")) {
                Usuario user = _usuarioService.loguearUsuarioPorEmail(usuario);
                if (user != null) {
                    //verifica si necesita redirigir a una pagina
                    Session["usuarioSesionEmail"] = user.Email;
                    Session["usuarioSesionNombre"] = user.Nombre;
                    Session["usuarioSesionApellido"] = user.Apellido;
                    Session["usuarioSesionId"] = user.IdUsuario;
                    if (Session["Action"] == null)
                        return RedirectToAction("Index", "Usuario"); /*redirije al Home*/
                    else {
                        string action = Session["Action"] as string;
                        Session.Remove("Action");
                        return RedirectToAction(action);
                    }
                }
                else {
                    TempData["Error"] = "Error de usuario y/o contraseña";
                }
            }
            else {
                var message = string.Join(" | ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage));

                //Return Status Code:
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }
            return RedirectToAction("Login", "Home");

        }

        //Proceso de registro de usuario
        [HttpPost]
        [ValidateAntiForgeryToken]  //Para prevenir ataques CSRF
        public ActionResult RegistrarUsuario([Bind(Include = "Nombre, Apellido, Email, Contrasenia")] Usuario usuario) {
            if (ModelState.IsValid) {
                String nombre = usuario.Nombre;
                String apellido = usuario.Apellido;
                String email = usuario.Email;
                String contrasenia = usuario.Contrasenia;
                String contrasenia2 = Request["Contrasenia2"];

                //Chequeo en la salida los datos que se recibieron
                System.Diagnostics.Debug.WriteLine("Datos recibidos del formulario: " + nombre + " " + apellido + " " + email + " "
                                                         + contrasenia + " " + contrasenia2);

                Usuario user = _usuarioService.registrarUsuario(new Usuario(usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Contrasenia), contrasenia2);
                string mensajeError = "";
                if (user != null) {
                    Session["usuarioSesionEmail"] = user.Email;
                    Session["usuarioSesionNombre"] = user.Nombre;
                    Session["usuarioSesionApellido"] = user.Apellido;
                    Session["usuarioSesionId"] = user.IdUsuario;
                    return RedirectToAction("Index", "Usuario"); /*redirije al Home*/
                }
                else {
                    //Informar de alguna forma que no se pudo registrar el usuario
                    mensajeError = _usuarioService.mostrarMensajeDeError();
                    TempData["Error"] = mensajeError;
                    return RedirectToAction("Registracion", usuario);
                }
            }
            else {
                TempData["Error"] = null;
                return View("Registracion", usuario);
            }
        }
    }
}




