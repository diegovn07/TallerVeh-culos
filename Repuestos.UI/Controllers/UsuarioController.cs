using System;
using System.Web.Mvc;
using Utilidades;

namespace Repuestos.UI.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            MUsuarios App = new MUsuarios();
            return View(App.GetAll());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            MUsuarios App = new MUsuarios();
            return View(App.GetOneById(id));
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Modelo.Usuario nuevo = new Modelo.Usuario {
                    nombre = collection["nombre"],
                    PApellido = collection["PApellido"],
                    SApellido = collection["SApellido"],
                    correo = collection["correo"],
                    contrasena = collection["contrasena"]
                };
                MUsuarios App = new MUsuarios();
                App.Insert(nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            MUsuarios App = new MUsuarios();
            return View(App.GetOneById(id));
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                MUsuarios App = new MUsuarios();
                App.Edit(id, collection["nombre"], collection["PApellido"], collection["SApellido"], collection["correo"], collection["contrasena"]);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            MUsuarios App = new MUsuarios();
            return View(App.GetOneById(id));
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                MUsuarios App = new MUsuarios();
                App.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
