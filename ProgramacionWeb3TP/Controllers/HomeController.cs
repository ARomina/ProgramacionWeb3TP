using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Models.Repositories;
using ProgramacionWeb3TP.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;


namespace ProgramacionWeb3TP.Controllers{


    public class HomeController : Controller {

        sUsuario usuarioServiceImpl = new sUsuario();

        public static List<Usuario> listUsuarios;
        // private UsuarioRepository usuarioRepository = new UsuarioRepository();

        //Sin estar loggueados

        // GET: Home

        

        /*LOGIN*/
        public ActionResult Index() {
            return View();
        }
        



        //Login dirige a página principal
        [HttpGet]
        public ActionResult Login() {

            return View();
        }


        /*Validación del Usuario Ingresado*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerificarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario user = usuarioServiceImpl.loguearUsuarioPorEmail(usuario);
                if (user != null)
                {
                    //verifica si necesita redirigir a una pagina
                    Session["usuarioEnSesion"] = user.Email;
                    if (Session["Action"] == null)
                        return RedirectToAction("Inicio"); /*redirije al Home*/
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

        }

        //VERIFICAR SESION
        public Boolean comprobarUsuario(String action)
        {
            //si el usuario existe devuelve true, sino crea la variable de sesion con el action al que tiene que volver
            if (Session["usuarioEnSesion"] != null)
            {
                string usuario = Session["usuarioEnSesion"] as string;
                var usuarioExistente = usuarioServiceImpl.buscarUsuarioPorEmail(usuario);
                if (usuarioExistente != null)
                {
                    return true;
                }
            }

            Session["Action"] = action;
            return false;
        }

        /*
        //Validar Login
        [HttpPost]
        public ActionResult ValidarLogin(Email email, Contrasenia contrasenia)
        {

            Context ctx = new Context();

            Usuario usuarioEncontrado = new Usuario();

            {

                usuarioEncontrado = ctx.Usuario.Where(u => u.Email == usuario.Email
                                                   && u.Contrasenia == usuario.Contrasenia).SingleOrDefault();

                {
                    if (usuarioObtenido == null)
                    {
                        us.LoginErrorMessage = "Usuario o Password Incorrectos.";
                        return View("Login", us);
                    }
                    else
                        {
                         
                        }

                }

            }
            return View();
         
        }
        */
        /*      //Registro
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
           */
    }
        
    }




