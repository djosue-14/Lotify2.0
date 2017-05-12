using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Clientes
{
    public class EstadoClienteController : Controller
    {
        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Estado Clientes";
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}