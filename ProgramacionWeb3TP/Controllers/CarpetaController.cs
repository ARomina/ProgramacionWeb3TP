using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class CarpetaController : Controller {
        
        // GET: Carpeta
        //Estando logueados --> Listado de carpetas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult MisCarpetas() {
            return View();
        }
    }
}