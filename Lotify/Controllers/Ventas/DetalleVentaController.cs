using Lotify.Models;
using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Ventas
{
    public class DetalleVentaController : Controller
    {
        private ApplicationDbContext dbCtx;
        private DetalleVenta detalle;

        public DetalleVentaController()
        {
            dbCtx = new ApplicationDbContext();
            detalle = new DetalleVenta();
        }

        // GET: DetalleVenta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}