using Lotify.Models;
using Lotify.Models.Lotes;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Lotes
{
    public class LoteController : Controller
    {

        private ApplicationDbContext dbCtx;
        private Lote Lotes;

        public LoteController()
        {
            dbCtx = new ApplicationDbContext();
            Lotes = new Lote();
        }
        
        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Lotes";

            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {

            var Lotes = dbCtx.Lote.Select(c => new {
                c.Id,
                c.NumeroLote,
                c.Precio,
                Medida = new {
                    c.Medida.Id,
                    c.Medida.Ancho,
                    c.Medida.Largo,
                },
                EstadoLote = new {
                    c.EstadoLote.Id,
                    c.EstadoLote.NombreEstado,
                },
                Lotificadora = new {
                    c.Lotificadora.Id,
                    c.Lotificadora.NombreLotificadora,
                    c.Lotificadora.Direccion,
                },
                Manzana = new {
                    c.Manzana.Id,
                    c.Manzana.NombreManzana,
                },
                Area = new {
                    c.Area.Id,
                    c.Area.NombreArea,
                },
               
            });

            return Json(Lotes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var Lotes = dbCtx.Lote.FirstOrDefault(p => p.Id == id);

            return Json(Lotes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Ingresar Lotes";

            LoteViewModels model = new LoteViewModels();

            model.Medidas = dbCtx.Medida.ToList();
            model.EstadoLote = dbCtx.EstadoLote.ToList();
            model.Lotificadora = dbCtx.Lotificadora.ToList();
            model.Area = dbCtx.Area.ToList();
            model.Manzana = dbCtx.Manzana.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LoteViewModels model)
        {
            if (ModelState.IsValid)
            {
                Lotes.NumeroLote = model.NumeroLote;
                Lotes.Precio = model.Precio;
                Lotes.MedidaId = model.MedidaId;
           
                //Seleccionamos el estad cliente que sea 'ACTIVO' y luego lo agregamos a FK EstadoClienteId
                EstadoLote estado = dbCtx.EstadoLote.FirstOrDefault(e => e.NombreEstado == "Activo");
                Lotes.EstadoLoteId = estado.Id;

                Lotes.LotificadoraId = model.LotificadoraId;
                Lotes.ManzanaId = model.ManzanaId;
                Lotes.AreaId = model.AreaId;

                dbCtx.Lote.Add(Lotes);
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Lotes";

            LoteViewModels model = new LoteViewModels();

            Lotes = dbCtx.Lote.FirstOrDefault(a => a.Id == id);
            model.NumeroLote = Lotes.NumeroLote;
            model.Precio = Lotes.Precio ;
            model.MedidaId = Lotes.MedidaId;
            model.EstadoLoteId = Lotes.EstadoLoteId;
            model.LotificadoraId = Lotes.LotificadoraId;
            model.ManzanaId = Lotes.ManzanaId;
            model.AreaId = Lotes.AreaId;

            model.Medidas = dbCtx.Medida.ToList();
            model.EstadoLote = dbCtx.EstadoLote.ToList();
            model.Lotificadora = dbCtx.Lotificadora.ToList();
            model.Area = dbCtx.Area.ToList();
            model.Manzana = dbCtx.Manzana.ToList();

            dbCtx.SaveChanges();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(LoteViewModels model)
        {
            if (ModelState.IsValid)
            {
                Lotes = dbCtx.Lote.FirstOrDefault(a => a.Id == model.Id);
                Lotes.NumeroLote = model.NumeroLote;
                Lotes.Precio = model.Precio;

                Lotes.MedidaId = model.MedidaId;
                Lotes.EstadoLoteId = model.EstadoLoteId;
                Lotes.LotificadoraId = model.LotificadoraId;
                Lotes.ManzanaId = model.ManzanaId;
                Lotes.AreaId = model.AreaId;

                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(LoteViewModels model)
        {
            var estado = (from p in dbCtx.Lote where p.Id == model.Id select p).FirstOrDefault();

            dbCtx.Lote.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}