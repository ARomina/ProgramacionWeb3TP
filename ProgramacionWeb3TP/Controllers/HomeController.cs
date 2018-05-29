using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramacionWeb3TP.Controllers{
    public class HomeController : Controller {

        //Sin estar loggueados

        // GET: Home
        public ActionResult Index() {
            return View();
        }


        //Login, recibo la vista
        [HttpGet]
        public ActionResult Login() {



            return View();2
        }

        //Proceso los datos ingresados para validar el usuario ingresado
        [HttpPost]
        public ActionResult Login(){



            return View();
        }




        //Registro
        public ActionResult Registracion() {
            return View();
        }
    }
}