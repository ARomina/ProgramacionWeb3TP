using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class TareasController : Controller {
        
        // GET: Tarea
        //Estando logueados --> Listado de tareas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult Index() {
            return View();
        }

        //Vista
        public ActionResult Crear() {
            return View();
        }

        //Procesar creación de nueva tarea
        public ActionResult CreacionTarea() {
            return RedirectToAction("Index", "Tareas");
        }

        //Vista
        public ActionResult Detalle() {
            return View();
        }

        //Vista
        public ActionResult Modificar() {
            return View();
        }

        //Procesar modificación de tarea
        public ActionResult ModificacionTarea() {
            return RedirectToAction("Index", "Tareas");
        }

        //Vista
        public ActionResult Eliminar() {
            return View();
        }

        //Procesar eliminación de tarea
        public ActionResult EliminacionTarea() {
            return RedirectToAction("Index", "Tareas");
        }
    }
}