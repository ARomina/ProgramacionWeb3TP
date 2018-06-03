using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class CarpetasController : Controller {
        
        // GET: Carpeta
        //Estando logueados --> Listado de carpetas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult Index() {
            return View();
        }

        //Vista
        public ActionResult Crear() {
            return View();
        }

        //Procesar creación de carpeta
        public ActionResult CreacionCarpeta() {
            return RedirectToAction("Index", "Carpetas");
        }

        //Vista
        public ActionResult Tareas() {
            return View();
        }

        //Vista
        public ActionResult Eliminar() {
            return View();
        }

        //Procesar eliminación de carpeta
        public ActionResult EliminacionCarpeta() {
            return RedirectToAction("Index", "Carpetas");
        }
    }
}