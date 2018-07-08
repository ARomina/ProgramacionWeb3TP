using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index(int? id) {
            //public ActionResult Index(int? id) {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }
            else {
                /*if (Request.Cookies["CookieUsuario"] != null) {
                    userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
                }
                else {
                    userIdInSession = (int)Session["usuarioSesionId"];
                }*/
                userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userIdInSession);
            }

            List<Tarea> listaTareas;
            if (id != null) {
                if (id == 0) {
                    listaTareas = await _tareaService.listarTareasIncompletasPorUsuarioAsync(userIdInSession);
                }
                else {
                    listaTareas = await _tareaService.listarTareasCompletasPorUsuarioAsync(userIdInSession);
                }
            }else {
                listaTareas = await _tareaService.listarTareasAsync(userIdInSession);
            }
            //List<Tarea> listaTareas = _tareaService.listarTareas(userIdInSession);
            return View(listaTareas);
        }

        //Vista
        public ActionResult Crear() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }

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
            if (ModelState.IsValid) {
                DateTime parsedFechaFin = DateTime.Parse(Request["FechaFin"]);
                short parsedPrioridad = short.Parse(Request["Prioridad"]);
                short parsedCompletada = short.Parse(Request["Completada"]);
                tarea.FechaFin = parsedFechaFin;

                if (parsedPrioridad == 0) {
                    tarea.Prioridad = 4;
                }else {
                    tarea.Prioridad = parsedPrioridad;
                }

                tarea.Completada = parsedCompletada;

                System.Diagnostics.Debug.WriteLine("Crear Tarea - IdCarpeta: " + tarea.IdCarpeta);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - Nombre: " + tarea.Nombre);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - Descripcion: " + tarea.Descripcion);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - EstimadoHoras: " + tarea.EstimadoHoras);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - FechaFin: " + parsedFechaFin);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - Prioridad: " + parsedPrioridad);
                System.Diagnostics.Debug.WriteLine("Crear Tarea - Completada: " + parsedCompletada);

                /*if (Request.Cookies["CookieUsuario"] != null) {
                    userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
                }
                else {
                    userIdInSession = (int)Session["usuarioSesionId"];
                }*/
                userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);
                Usuario usuarioActual = _usuarioService.ObtenerUsuarioPorId(userIdInSession);
                Tarea tareaNueva = _tareaService.CrearTarea(tarea, usuarioActual);
                if (tareaNueva == null) {
                    TempData["Error"] = MENSAJE_ERROR_NO_SE_PUDO_CREAR;
                }
            }
            return RedirectToAction("Listado", "Tareas", new { idCarpeta = tarea.IdCarpeta });
        }

        //Procesar creacion de nueva carpeta (sin carpeta definida de un principio)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreacionNuevaTarea(Tarea tarea) {
            /*if (Request.Cookies["CookieUsuario"] != null) {
                userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            }
            else {
                userIdInSession = (int)Session["usuarioSesionId"];
            }*/
            userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);

            int carpetaIdValue = carpetaIdValue = int.Parse(Request.Form["carpetaId"].ToString());
            if (ModelState.IsValid) {
                DateTime parsedFechaFin = DateTime.Parse(Request["FechaFin"]);
                short parsedPrioridad = short.Parse(Request["Prioridad"]);
                short parsedCompletada = short.Parse(Request["Completada"]);
                tarea.FechaFin = parsedFechaFin;

                if (parsedPrioridad == 0) {
                    tarea.Prioridad = 4;
                }
                else {
                    tarea.Prioridad = parsedPrioridad;
                }

                tarea.Completada = parsedCompletada;

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
            }
            return RedirectToAction("Listado", "Tareas", new { idCarpeta = carpetaIdValue });
        }

        //Vista
        public ActionResult CrearNuevaTarea() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }

            /*if (Request.Cookies["CookieUsuario"] != null) {
                userIdInSession = int.Parse(Session["usuarioSesionId"] as String);
            }
            else {
                userIdInSession = (int)Session["usuarioSesionId"];
            }*/
            userIdInSession = Convert.ToInt32(Session["usuarioSesionId"]);
            List<Carpeta> lista = _carpetaService.ObtenerCarpetasPorUsuario(userIdInSession);
            foreach (Carpeta c in lista) {
                ViewBag.listaCarpetas = lista;
            }
            return View();
        }

        //Vista
        public ActionResult Detalle() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }

            string idTarea = Request.QueryString["idTarea"];
            System.Diagnostics.Debug.WriteLine("Ver Tarea" + idTarea);
            Tarea tareaAVer = _tareaService.ObtenerTareaPorId(int.Parse(idTarea));
            return View(tareaAVer);
        }

        //Vista
        public ActionResult Modificar() {
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }

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
            if (Session["usuarioSesionId"] == null) {
                String userNameInSession;
                userNameInSession = "No user in session";
                System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                Session["returnPath"] = Request.RawUrl;
                return RedirectToAction("Login", "Home");
            }

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
        
        //Vista
        public ActionResult Listado(int? idCarpeta)
        {
            if (idCarpeta != null) {
                if (Session["usuarioSesionId"] == null) {
                    String userNameInSession;
                    userNameInSession = "No user in session";
                    System.Diagnostics.Debug.WriteLine("Home - Tareas: " + userNameInSession);
                    Session["returnPath"] = Request.RawUrl;
                    return RedirectToAction("Login", "Home");
                }
            }else {
                return RedirectToAction("Login", "Home");
            }

            List<Tarea> tareas = _tareaService.ObtenerTareasPorCarpeta(idCarpeta);
            TempData["idCarpeta"] = idCarpeta;
            return View(tareas);
        }

        //Procesar Completar Tarea
        public ActionResult CompletarTarea(int id) {
            System.Diagnostics.Debug.WriteLine("Completar Tarea" + id);
            _tareaService.completarTarea(id);
            return RedirectToAction("Index", "Usuario");
        }

        //Procesar Crear Comentario
        [HttpPost]
        public ActionResult CrearComentario([Bind]int idTarea, [Bind]string texto) {
            _tareaService.CrearComentario(idTarea, texto);
            return RedirectToAction("Detalle", new { idTarea });
        }
    }
}