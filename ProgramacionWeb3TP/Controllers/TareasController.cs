using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers {
    public class TareasController : Controller {

        private TareaService _tareaService = new TareaService();
        private UsuarioService _usuarioService = new UsuarioService();

        private int userIdInSession;

        //Variables para los mensajes de error
        private String MENSAJE_ERROR_NO_SE_PUDO_CREAR = "No se pudo crear la tarea";

        // GET: Tarea
        //Estando logueados --> Listado de tareas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult Index() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
            }
            else {
                userIdInSession = (int)Session["usuarioSesionId"];
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userIdInSession);
            }
            return View();
        }

        //Vista
        public ActionResult Crear() {
            string idCarpetaCREAR = Request.QueryString["idCarpeta"];
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpetaCREAR: " + idCarpetaCREAR);
            Tarea tareaACrear = new Tarea(int.Parse(idCarpetaCREAR));
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpetaACREAR: " + tareaACrear.IdCarpeta);
            return View(tareaACrear);
        }

        //Procesar creación de nueva tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreacionTarea(Tarea tarea) {
            DateTime parsedFechaFin = DateTime.Parse(Request["FechaFin"]);
            short parsedPrioridad = short.Parse(Request["Prioridad"]);
            short parsedCompletada = short.Parse(Request["Completada"]);
            tarea.FechaFin = parsedFechaFin;
            tarea.Prioridad = parsedPrioridad;
            tarea.Completada = parsedCompletada;

            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + tarea.IdCarpeta);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Nombre: " + tarea.Nombre);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Descripcion: " + tarea.Descripcion);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - EstimadoHoras: " + tarea.EstimadoHoras);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - FechaFin: " + parsedFechaFin);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Prioridad: " + parsedPrioridad);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Completada: " + parsedCompletada);

            userIdInSession = (int)Session["usuarioSesionId"];
            Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession);
            Tarea tareaNueva = _tareaService.CrearTarea(tarea, usuarioActual);
            if (tareaNueva == null) {
                TempData["Error"] = MENSAJE_ERROR_NO_SE_PUDO_CREAR;
            }
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

        public ActionResult Listado(int idCarpeta)
        {
            List<Tarea> tareas = _tareaService.ObtenerTareasPorCarpeta(idCarpeta);
            TempData["idCarpeta"] = idCarpeta;
            return View(tareas);
        }
    }
}