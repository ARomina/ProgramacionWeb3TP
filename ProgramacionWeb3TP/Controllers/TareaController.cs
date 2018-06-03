using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class TareaController : Controller {
        
        // GET: Tarea
        //Estando logueados --> Listado de tareas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult MisTareas() {
            return View();
        }

        //Vista
        public ActionResult NuevaTarea() {
            return View();
        }

        //Procesar creación de nueva tarea
        public ActionResult CreacionNuevaTarea() {
            return RedirectToAction("MisTareas", "Tarea");
        }

        //Vista
        public ActionResult VerDetalleTarea() {
            return View();
        }

        //Vista
        public ActionResult ModificarTarea() {
            return View();
        }

        //Procesar modificación de tarea
        public ActionResult ModificacionTarea() {
            return RedirectToAction("MisTareas", "Tarea");
        }

        //Vista
        public ActionResult EliminarTarea() {
            return View();
        }

        //Procesar eliminación de tarea
        public ActionResult EliminacionTarea() {
            return RedirectToAction("MisTareas", "Tarea");
        }
    }
}