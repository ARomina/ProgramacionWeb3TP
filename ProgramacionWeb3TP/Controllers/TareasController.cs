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
        private CarpetaService _carpetaService = new CarpetaService();

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
                userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userIdInSession);
            }
            userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            List<Tarea> listaTareas = _tareaService.listarTareas(userIdInSession);
            return View(listaTareas);
        }

        //Vista
        public ActionResult Crear() {
            string idCarpeta = Request.QueryString["idCarpeta"];
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + idCarpeta);
            Tarea tareaACrear = new Tarea(int.Parse(idCarpeta));
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpetaACREAR: " + tareaACrear.IdCarpeta);
            return View(tareaACrear);
        }

        //Procesar creación de nueva tarea (desde dentro de carpeta)
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

            userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession);
            Tarea tareaNueva = _tareaService.CrearTarea(tarea, usuarioActual);
            if (tareaNueva == null) {
                TempData["Error"] = MENSAJE_ERROR_NO_SE_PUDO_CREAR;
            }
            return RedirectToAction("Listado", "Tareas", new { idCarpeta = tarea.IdCarpeta });
        }

        //Procesar creacion de nueva carpeta (sin carpeta definida de un principio)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreacionNuevaTarea(Tarea tarea) {
            userIdInSession = int.Parse(Session["usuarioSesionId"] as String);

            DateTime parsedFechaFin = DateTime.Parse(Request["FechaFin"]);
            short parsedPrioridad = short.Parse(Request["Prioridad"]);
            short parsedCompletada = short.Parse(Request["Completada"]);
            tarea.FechaFin = parsedFechaFin;
            tarea.Prioridad = parsedPrioridad;
            tarea.Completada = parsedCompletada;

            int carpetaIdValue = int.Parse(Request.Form["carpetaId"].ToString());
            if (carpetaIdValue == 0) {
                carpetaIdValue = _carpetaService.buscarCarpetaGeneralPorUsuarioId(userIdInSession);
            }
            tarea.IdCarpeta = carpetaIdValue;
           
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpetaValue: " + carpetaIdValue);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + tarea.IdCarpeta);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Nombre: " + tarea.Nombre);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Descripcion: " + tarea.Descripcion);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - EstimadoHoras: " + tarea.EstimadoHoras);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - FechaFin: " + parsedFechaFin);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Prioridad: " + parsedPrioridad);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Completada: " + parsedCompletada);

            Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession);
            Tarea tareaNueva = _tareaService.CrearTarea(tarea, usuarioActual);
            if (tareaNueva == null) {
                TempData["Error"] = MENSAJE_ERROR_NO_SE_PUDO_CREAR;
            }
            return RedirectToAction("Listado", "Tareas", new { idCarpeta = carpetaIdValue });
        }

        //Vista
        public ActionResult CrearNuevaTarea() {
            userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            List<Carpeta> lista = _carpetaService.ObtenerCarpetasPorUsuario(userIdInSession);
            foreach (Carpeta c in lista) {
                ViewBag.listaCarpetas = lista;
            }
            return View();
        }

        //Vista
        public ActionResult Detalle() {
            string idTarea = Request.QueryString["idTarea"];
            System.Diagnostics.Debug.WriteLine("Ver Tarea" + idTarea);
            Tarea tareaAVer = _tareaService.ObtenerTareaPorId(int.Parse(idTarea));
            return View(tareaAVer);
        }

        //Vista
        public ActionResult Modificar() {
            string idTarea = Request.QueryString["idTarea"];
            System.Diagnostics.Debug.WriteLine("Modificar Tarea" + idTarea);
            Tarea tareaAModificar = _tareaService.ObtenerTareaPorId(int.Parse(idTarea));
            return View(tareaAModificar);
        }

        //Procesar modificación de tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificacionTarea(Tarea tarea) {
            System.Diagnostics.Debug.WriteLine("Modificacion Tarea" + tarea.IdTarea);
            return RedirectToAction("Index", "Tareas");
        }

        //Vista
        public ActionResult Eliminar() {
            string idTarea = Request.QueryString["idTarea"];
            System.Diagnostics.Debug.WriteLine("Eliminar Tarea" + idTarea);
            Tarea tareaAEliminar = _tareaService.ObtenerTareaPorId(int.Parse(idTarea));
            return View(tareaAEliminar);
        }

        //Procesar eliminación de tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminacionTarea(Tarea tarea) {
            System.Diagnostics.Debug.WriteLine("Eliminacion Tarea" + tarea.IdTarea);
            _tareaService.eliminarTarea(tarea.IdTarea);
            return RedirectToAction("Listado", "Tareas");
        }

        public ActionResult Listado(int idCarpeta)
        {
            List<Tarea> tareas = _tareaService.ObtenerTareasPorCarpeta(idCarpeta);
            TempData["idCarpeta"] = idCarpeta;
            return View(tareas);
        }
    }
}