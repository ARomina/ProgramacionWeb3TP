using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3TP.Models;
using ProgramacionWeb3TP.Services;

namespace ProgramacionWeb3TP.Controllers {
    public class CarpetasController : Controller {

        // Service
        private CarpetaService _carpetaService = new CarpetaService();

        //TODO:Fake
        private readonly int fakeUserId = 8;

        // GET: Carpeta
        //Estando logueados --> Listado de carpetas
        //Chequear si el usuario esta en sesión, sino mostrar pantalla de que no esta logueado, etc
        public ActionResult Index()
        {
            List<Carpeta> carpetas = _carpetaService.ObtenerCarpetasPorUsuario(fakeUserId);
            return View(carpetas);
        }

        //Vista
        public ActionResult Crear()
        {
            return View();
        }

        //Procesar creación de carpeta
        public ActionResult CreacionCarpeta(Carpeta carpeta)
        {
            carpeta.IdUsuario = fakeUserId;
            _carpetaService.CrearCarpeta(carpeta);

            return RedirectToAction("Index");
        }
        
        //Vista
        public ActionResult Detalle(int idCarpeta)
        {
            return RedirectToAction("Listado", "Tareas", new { idCarpeta });
        }

        //Vista
        public ActionResult Eliminar(int idCarpeta)
        {
            Carpeta carpeta = _carpetaService.ObtenerCarpetaPorId(idCarpeta);
            return View(carpeta);
        }

        //Procesar eliminación de carpeta
        public ActionResult EliminacionCarpeta(Carpeta carpeta)
        {
            _carpetaService.EliminarCarpeta(carpeta.IdCarpeta);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int idCarpeta)
        {
            Carpeta carpeta = _carpetaService.ObtenerCarpetaPorId(idCarpeta);
            return View(carpeta);
        }

        public ActionResult EdicionCarpeta(Carpeta carpeta)
        {
            _carpetaService.EditarCarpeta(carpeta);
            return RedirectToAction("Index");
        }
    }
}