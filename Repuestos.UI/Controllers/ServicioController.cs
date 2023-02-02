using System.Collections.Generic;
using System.Web.Mvc;
using Utilidades;

namespace Servicios.UI.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Index()
        {
            MServicios App = new MServicios();
            return View(App.GetAll());
        }

        // GET: Servicio/Details/5
        public ActionResult Details(string id)
        {
            MServicios App = new MServicios();
            return View(App.GetOneById(id));
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Modelo.Servicio nuevo = new Modelo.Servicio
                {
                    descripcion = collection["descripcion"],
                    precio_venta = double.Parse(collection["precio_venta"])
                };
                MServicios App = new MServicios();
                App.Insert(nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(string id)
        {
            MServicios App = new MServicios();
            return View(App.GetOneById(id));
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                MServicios App = new MServicios();
                App.Edit(id, collection["descripcion"], double.Parse(collection["precio_venta"]));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(string id)
        {
            MServicios App = new MServicios();
            return View(App.GetOneById(id));
        }

        // POST: Servicio/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                MServicios App = new MServicios();
                App.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ObtenerServicios()
        {
            MServicios App = new MServicios();
            IEnumerable<Modelo.Servicio> listaServicios = App.GetAll();
            return PartialView("~/Views/Partial/_ServiciosPartial.cshtml", listaServicios);
        }

    }
}
