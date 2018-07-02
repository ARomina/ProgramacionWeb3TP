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
                userIdInSession = (int)Session["usuarioSesionId"];
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userIdInSession);
            }
            userIdInSession = (int)Session["usuarioSesionId"];
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

            userIdInSession = (int)Session["usuarioSesionId"];
            Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession);
            Tarea tareaNueva = _tareaService.CrearTarea(tarea, usuarioActual);
            if (tareaNueva == null) {
                TempData["Error"] = MENSAJE_ERROR_NO_SE_PUDO_CREAR;
            }
            return RedirectToAction("Listado", "Tareas", tarea.IdCarpeta);
        }

        //Procesar creacion de nueva carpeta (sin carpeta definida de un principio)
        public void CreacionNuevaTarea(Tarea tarea) {
            DateTime parsedFechaFin = DateTime.Parse(Request["FechaFin"]);
            short parsedPrioridad = short.Parse(Request["Prioridad"]);
            short parsedCompletada = short.Parse(Request["Completada"]);
            tarea.FechaFin = parsedFechaFin;
            tarea.Prioridad = parsedPrioridad;
            tarea.Completada = parsedCompletada;

            //int carpetaId = int.Parse(Request["carpetaId"]);
            //tarea.IdCarpeta = carpetaId;
            /*var selectedText = carpetaId.Items[Select1.SelectedIndex].Text.Trim();

            System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + carpetaIdValue);
            //System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + tarea.IdCarpeta);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Nombre: " + tarea.Nombre);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Descripcion: " + tarea.Descripcion);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - EstimadoHoras: " + tarea.EstimadoHoras);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - FechaFin: " + parsedFechaFin);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Prioridad: " + parsedPrioridad);
            System.Diagnostics.Debug.WriteLine("Crear Tarea - Completada: " + parsedCompletada);
            */
        }

        //Vista
        public ActionResult CrearNuevaTarea() {
            //System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpetaACREAR: " + );
            userIdInSession = (int)Session["usuarioSesionId"];
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