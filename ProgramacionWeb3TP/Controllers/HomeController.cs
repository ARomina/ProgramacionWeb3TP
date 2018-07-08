using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProgramacionWeb3TP.Models.Entities;
using System.Web.Security;
using System.Text;

namespace ProgramacionWeb3TP.Controllers {


    public class HomeController : Controller {
        private UsuarioService _usuarioService = new UsuarioService();

        //Sin estar loggueados

        // GET: Home
        public ActionResult Index() {
            if (Request.Cookies["CookieUsuario"] != null) {
                if (Session["usuarioSesionId"] == null) {

                    Session["usuarioSesionEmail"] = UnprotectCookieInfo(Request.Cookies["CookieUsuario"]["CookieUsuarioEmail"], "CookieInfo");
                    Session["usuarioSesionNombre"] = UnprotectCookieInfo(Request.Cookies["CookieUsuario"]["CookieUsuarioNombre"], "CookieInfo");
                    Session["usuarioSesionApellido"] = UnprotectCookieInfo(Request.Cookies["CookieUsuario"]["CookieUsuarioApellido"], "CookieInfo");
                    Session["usuarioSesionId"] = UnprotectCookieInfo(Request.Cookies["CookieUsuario"]["CookieUsuarioId"], "CookieInfo");
                }
                return RedirectToAction("Index", "Usuario");
            }
            else {
                return View();
            }
            /*if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Index: " + userNameInSession);
            }
            else {
                int userIdInSession;
                userIdInSession = (int) Session["usuarioSesionId"];
                System.Diagnostics.Debug.WriteLine("Home: " + userIdInSession);
            }*/
            //return View();
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
        public ActionResult VerificarUsuario(Usuario usuario, bool rememberMe = false) {
            System.Diagnostics.Debug.WriteLine("Login - Remember Me: " + rememberMe);

            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Contrasenia")) {
                Usuario user = _usuarioService.loguearUsuarioPorEmail(usuario);
                if (user != null) {
                    //Se setea la cookie si el remember me es true
                    if (rememberMe) {
                        HttpCookie userCookie = new HttpCookie("CookieUsuario");
                        userCookie["CookieUsuarioId"] = ProtectCookieInfo(user.IdUsuario.ToString(), "CookieInfo");
                        userCookie["CookieUsuarioNombre"] = ProtectCookieInfo(user.Nombre, "CookieInfo");
                        userCookie["CookieUsuarioApellido"] = ProtectCookieInfo(user.Apellido, "CookieInfo");
                        userCookie["CookieUsuarioEmail"] = ProtectCookieInfo(user.Email, "CookieInfo");
                        userCookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(userCookie);
                        System.Diagnostics.Debug.WriteLine("Login - Cookie Usuario Id: " + userCookie["CookieUsuarioId"]);
                    }

                    //verifica si necesita redirigir a una pagina
                    Session["usuarioSesionEmail"] = user.Email;
                    Session["usuarioSesionNombre"] = user.Nombre;
                    Session["usuarioSesionApellido"] = user.Apellido;
                    Session["usuarioSesionId"] = user.IdUsuario;
                    if (Session["ReturnPath"] == null)
                        return RedirectToAction("Index", "Usuario"); /*redirije al Home*/
                    else {
                        string path = Session["ReturnPath"] as string;
                        Session.Remove("ReturnPath");
                        Response.Redirect(path);
                        //return RedirectToAction(path);
                    }
                }
                else {
                    TempData["Error"] = "Error de usuario y/o contraseña";
                }
            }
            
            
        /*}
            else {
                var message = string.Join(" | ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage));

                //Return Status Code:
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }*/
            return RedirectToAction("Login", "Home");

        }

        //Proceso de registro de usuario
        [HttpPost]
        [ValidateAntiForgeryToken]  //Para prevenir ataques CSRF
        public ActionResult RegistrarUsuario([Bind(Include = "Nombre, Apellido, Email, Contrasenia")] Usuario usuario) {
            var encodedResponse = Request.Form["g-Recaptcha-Response"];
            bool isCaptchaValid = ReCaptchaClass.Validate(encodedResponse);
            System.Diagnostics.Debug.Write("Registracion - Captcha: ", isCaptchaValid.ToString());

            if (!isCaptchaValid) {
                TempData["Error"] = "El captcha es inválido";
            }
            else {
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
                    }
                }
                else {
                    TempData["Error"] = null;
                }
            }
             return View("Registracion", usuario);
            }

        //Metodos para encriptar y desencriptar la cookie
        public static string ProtectCookieInfo(string text, string purpose) {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = Encoding.UTF8.GetBytes(text);
            byte[] encodedValue = MachineKey.Protect(stream, purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        public static string UnprotectCookieInfo(string text, string purpose) {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = HttpServerUtility.UrlTokenDecode(text);
            byte[] decodedValue = MachineKey.Unprotect(stream, purpose);
            return Encoding.UTF8.GetString(decodedValue);
        }

    }
}




