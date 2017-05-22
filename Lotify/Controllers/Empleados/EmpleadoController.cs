using Lotify.Models;
using Lotify.Models.Empleados;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;



namespace Lotify.Controllers.Empleados
{
    public class EmpleadoController : Controller
    {
        private ApplicationDbContext dbCtx;
        private Empleado empleado;
        private ApplicationUserManager _userManager;

        public EmpleadoController()
        {
            dbCtx = new ApplicationDbContext();
            empleado = new Empleado();
        }

        public EmpleadoController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Empleados";

            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {

            var empleados = dbCtx.Empleado.Select(c => new {
                c.Id,
                c.Nombre,
                c.Apellido,
                c.Dpi,
                c.Genero,
                c.Direccion,
                c.FechaNacimiento,
                EstadoEmpleado = new
                {
                    c.EstadoEmpleado.Id,
                    c.EstadoEmpleado.NombreEstado,
                },
                CargoEmpleado = new
                {
                    c.CargoEmpleado.Id,
                    c.CargoEmpleado.NombreCargo,
                    c.CargoEmpleado.Sueldo,
                }
            });

            return Json(empleados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var empleado = dbCtx.Empleado.FirstOrDefault(p => p.Id == id);

            return Json(empleado, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Create()
        {
            ViewBag.Title = "Ingresar Empleado";

            EmpleadoViewModels model = new EmpleadoViewModels();
            model.Nombre = "";
            model.Apellido = "";
            model.Dpi = 0;
            model.Genero = "";
            model.Direccion = "";
            model.FechaNacimiento = new DateTime();
            model.CargoEmpleadoId = 0;
            model.CargoEmpleado = dbCtx.CargoEmpleado.ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmpleadoViewModels model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    empleado.Nombre = model.Nombre;
                    empleado.Apellido = model.Apellido;
                    empleado.Dpi = Convert.ToInt64(model.Dpi);
                    empleado.Genero = model.Genero;
                    empleado.Direccion = model.Direccion;
                    empleado.FechaNacimiento = model.FechaNacimiento;

                    //Seleccionamos el estadp del Empleado que sea 'ACTIVO' y luego lo agregamos a FK EstadoClienteId
                    EstadoEmpleado estado = dbCtx.EstadoEmpleado.FirstOrDefault(e => e.NombreEstado == "Activo");
                    empleado.EstadoEmpleadoId = estado.Id;

                    //Asignamos el cargo del empleado que se ha selecionado en la etiqueta select.
                    empleado.CargoEmpleadoId = model.CargoEmpleadoId;
                    empleado.UserId = user.Id;

                    //UserManager.AddToRole(user.Id, "User");

                    dbCtx.Empleado.Add(empleado);
                    dbCtx.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Empleado";

            EmpleadoViewModels model = new EmpleadoViewModels();

            empleado = dbCtx.Empleado.FirstOrDefault(a => a.Id == id);
            model.Nombre = empleado.Nombre;
            model.Apellido = empleado.Apellido;
            model.Dpi = empleado.Dpi;
            model.Genero = empleado.Genero;
            model.Direccion = empleado.Direccion;
            model.FechaNacimiento = empleado.FechaNacimiento;
            model.EstadoEmpleadoId = empleado.EstadoEmpleadoId;
            model.CargoEmpleadoId = empleado.CargoEmpleadoId;

            model.EstadoEmpleado = dbCtx.EstadoEmpleado.ToList();
            model.CargoEmpleado = dbCtx.CargoEmpleado.ToList();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(EmpleadoViewModels model)
        {
            if (ModelState.IsValid)
            {
                empleado = dbCtx.Empleado.FirstOrDefault(a => a.Id == model.Id);
                empleado.Nombre = model.Nombre;
                empleado.Apellido = model.Apellido;
                empleado.Dpi = model.Dpi;
                empleado.Genero = model.Genero;
                empleado.Direccion = model.Direccion;
                empleado.FechaNacimiento = model.FechaNacimiento;
                empleado.EstadoEmpleadoId = model.EstadoEmpleadoId;
                empleado.CargoEmpleadoId = model.CargoEmpleadoId;

                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(EmpleadoViewModels model)
        {
            var estado = (from p in dbCtx.Empleado where p.Id == model.Id select p).FirstOrDefault();

            dbCtx.Empleado.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}