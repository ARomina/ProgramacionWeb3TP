using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;


namespace ProgramacionWeb3TP.Controllers{
    public class HomeController : Controller {
        private UsuarioService _usuarioService = new UsuarioService();

        //Sin estar loggueados

        // GET: Home
        public ActionResult Index() {
            String userNameInSession;
            if (Session["usuarioEnSesion"] == null) {
                userNameInSession = "No user in session";
            }
            else {
                userNameInSession = (String) Session["usuarioEnSesion"];
            }
            System.Diagnostics.Debug.WriteLine("Home: " + userNameInSession);
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
            if (ModelState.IsValid) {
                Usuario user = _usuarioService.loguearUsuarioPorEmail(usuario);
                if (user != null) {
                    System.Diagnostics.Debug.WriteLine("Se logueo al usuario");
                    //verifica si necesita redirigir a una pagina
                    Session["usuarioEnSesion"] = user.Email;
                    Session["usuarioSesionNombre"] = user.Nombre;
                    Session["usuarioSesionApellido"] = user.Apellido;
                    Session["usuarioSesionId"] = user.IdUsuario;
                    //if (Session["Action"] == null)
                    return RedirectToAction("Index", "Usuario"); /*redirije al Home*/
                    //else {
                      //  string action = Session["Action"] as string;
                      //  Session.Remove("Action");
                      //  return RedirectToAction(action);
                    //}
                }else {
                    System.Diagnostics.Debug.WriteLine("No se logueo al usuario");
                    TempData["Error"] = "Error de usuario y/o contraseña";
                }
            }
            return RedirectToAction("Login", "Home");

        }

        //Proceso de registro de usuario
        [HttpPost]
        [ValidateAntiForgeryToken]  //Para prevenir ataques CSRF
        public ActionResult RegistrarUsuario([Bind(Include="IdUsuario, Nombre, Apellido, Email, Contrasenia")] Usuario usuario) {
            if (ModelState.IsValid) {
                String nombre = usuario.Nombre;
                String apellido = usuario.Apellido;
                String email = usuario.Email;
                String contrasenia = usuario.Contrasenia;
                String contrasenia2 = Request["Contrasenia2"];
                //String contrasenia2 = usuario.Contrasenia2;

                //Chequeo en la salida los datos que se recibieron
                System.Diagnostics.Debug.WriteLine("Datos recibidos del formulario: " + nombre + " " + apellido + " " + email + " "
                                                         + contrasenia + " " + contrasenia2);

                Usuario user = _usuarioService.registrarUsuario(new Usuario(usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Contrasenia), contrasenia2);
                if (user != null) {
                    Session["usuarioEnSesion"] = user.Email;
                    Session["usuarioSesionNombre"] = user.Nombre;
                    Session["usuarioSesionApellido"] = user.Apellido;
                    Session["usuarioSesionId"] = user.IdUsuario;
                    return RedirectToAction("Index", "Usuario"); /*redirije al Home*/
                    //return RedirectToAction("Index", "Home");
                }
                else {
                    //Informar de alguna forma que no se pudo registrar el usuario
                    return RedirectToAction("Index", "Home");
                } 
            }
            else {
                return View("Registracion", usuario);
            }
        }
 


        //-----------------------------------------------------------------
        //Esto es lo anterior, lo deje por las dudas

        // GET: Home

        /*LOGIN*/
        /*public ActionResult Index() {
            return View();
        }*/
        
        //Login dirige a página principal
        /*[HttpGet]
        public ActionResult Login() {

            return View();
        }*/

        /*Validación del Usuario Ingresado*/
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerificarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario user = _usuarioService.loguearUsuarioPorEmail(usuario);
                if (user != null)
                {
                    //verifica si necesita redirigir a una pagina
                    Session["usuarioEnSesion"] = user.Email;
                    if (Session["Action"] == null)
                        return RedirectToAction("Inicio"); /*redirije al Home*//*
                    else
                    {
                        string action = Session["Action"] as string;
                        Session.Remove("Action");
                        return RedirectToAction(action);
                    }
                }
                else
                {
                    TempData["Error"] = "Error de usuario y/o contraseña";
                }
            }
            return RedirectToAction("Login", "index");

        }*/

        //VERIFICAR SESION
        /*public Boolean comprobarUsuario(String action)
        {
            //si el usuario existe devuelve true, sino crea la variable de sesion con el action al que tiene que volver
            if (Session["usuarioEnSesion"] != null)
            {
                string usuario = Session["usuarioEnSesion"] as string;
                var usuarioExistente = _usuarioService.buscarUsuarioPorEmail(usuario);
                if (usuarioExistente != null)
                {
                    return true;
                }
            }

            Session["Action"] = action;
            return false;
        }*/
        
    }
        
    }




