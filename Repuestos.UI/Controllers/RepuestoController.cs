using System.Collections.Generic;
using System.Web.Mvc;
using Utilidades;

namespace Repuestos.UI.Controllers
{
    public class RepuestoController : Controller
    {
        // GET: Repuesto
        public ActionResult Index()
        {
            MRepuestos App = new MRepuestos();
            return View(App.GetAll());
        }

        // GET: Repuesto/Details/5
        public ActionResult Details(string id)
        {
            MRepuestos App = new MRepuestos();
            return View(App.GetOneById(id));
        }

        // GET: Repuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repuesto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Modelo.Repuesto nuevo = new Modelo.Repuesto
                {
                    descripcion = collection["descripcion"],
                    stock = int.Parse(collection["stock"]),
                    precio_venta = double.Parse(collection["precio_venta"])
                };
                MRepuestos App = new MRepuestos();
                App.Insert(nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repuesto/Edit/5
        public ActionResult Edit(string id)
        {
            MRepuestos App = new MRepuestos();
            return View(App.GetOneById(id));
        }

        // POST: Repuesto/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                MRepuestos App = new MRepuestos();
                App.Edit(id, collection["descripcion"], int.Parse(collection["stock"]), double.Parse(collection["precio_venta"]));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repuesto/Delete/5
        public ActionResult Delete(string id)
        {
            MRepuestos App = new MRepuestos();
            return View(App.GetOneById(id));
        }

        // POST: Repuesto/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                MRepuestos App = new MRepuestos();
                App.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ObtenerRepuestos()
        {
            MRepuestos App = new MRepuestos();
            IEnumerable<Modelo.Repuesto> listaRepuestos = App.GetAll();
            return PartialView("~/Views/Partial/_RepuestosPartial.cshtml", listaRepuestos);
        }
    }
}
