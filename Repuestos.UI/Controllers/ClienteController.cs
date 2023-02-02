using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utilidades;

namespace Clientes.UI.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            MClientes App = new MClientes();
            return View(App.GetAll());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(string id)
        {
            MClientes App = new MClientes();
            return View(App.GetOneById(id));
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Modelo.Cliente nuevo = new Modelo.Cliente
                {
                    nombre = collection["nombre"],
                    PApellido = collection["PApellido"],
                    SApellido = collection["SApellido"],
                    telefono = collection["telefono"],
                    correo = collection["correo"],
                    Reparaciones = new List<Modelo.Vehiculo>()
                };
                MClientes App = new MClientes();
                App.Insert(nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(string id)
        {
            MClientes App = new MClientes();
            return View(App.GetOneById(id));
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                MClientes App = new MClientes();
                App.Edit(id, collection["nombre"], collection["PApellido"], collection["SApellido"], collection["telefono"], collection["correo"]);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(string id)
        {
            MClientes App = new MClientes();
            return View(App.GetOneById(id));
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                MClientes App = new MClientes();
                App.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarVehiculo() {
            MClientes App = new MClientes();
            List<Modelo.Cliente> lstClientes = App.GetAll().ToList();
            List<SelectListItem> clientes = lstClientes.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre.ToString() + " " +d.PApellido.ToString() + " " + d.SApellido.ToString(),
                    Value = d.id.ToString(),
                    Selected = false
                };
            });
            ViewBag.clientes = clientes;
            return View();
        }

        [HttpPost]
        public ActionResult AgregarVehiculo(string Cliente, string Placa, string Marca, string Modelo, int Ano)
        {
            try
            {
                // TODO: Add insert logic here
                Modelo.Vehiculo nuevo = new Modelo.Vehiculo
                {
                    placa = Placa,
                    marca = Marca,
                    modelo = Modelo,
                    ano = Ano,
                    Cobros = new List<Modelo.Cobros>(),
                };
                MClientes App = new MClientes();
                App.AgregarVehiculo(Cliente, nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarReparacion()
        {
            MClientes App = new MClientes();
            List<Modelo.Cliente> lstClientes = App.GetAll().ToList();
            List<SelectListItem> clientes = lstClientes.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre.ToString() + " " + d.PApellido.ToString() + " " + d.SApellido.ToString(),
                    Value = d.id.ToString(),
                    Selected = false
                };
            });
            ViewBag.clientes = clientes;
            return View();
        }

        [HttpPost]
        public ActionResult AgregarReparacion(string Cliente, string Vehiculo, string Tipo, string Articulo, int Cantidad)
        {
            string resultado;
            try
            {
                // TODO: Add insert logic here
                MClientes App = new MClientes();
                resultado = App.AgregarReparacion(Cliente, Vehiculo, Tipo, Articulo, Cantidad);
                return PartialView("~/Views/Partial/_NotificacionPartial.cshtml", resultado);
            }
            catch
            {
                resultado = "3";
                return PartialView("~/Views/Partial/_NotificacionPartial.cshtml", resultado);
            }
        }

        [HttpGet]
        public ActionResult ObtenerVehiculos(string id)
        {
            MClientes App = new MClientes();
            List<Modelo.Vehiculo> listaVehiculos = App.GetAllVehiculos(id);
            return PartialView("~/Views/Partial/_VehiculosPartial.cshtml", listaVehiculos);
        }

        [HttpGet]
        public ActionResult cargarLista(string cliente,string vehiculo)
        {
            MClientes App = new MClientes();
            IEnumerable<Modelo.Cobros> listaCobros = App.GetCobros(cliente,vehiculo);
            double total = 0;
            if (listaCobros != null) {
                foreach (var item in listaCobros)
                {
                    total = total + double.Parse(item.total.ToString());
                }
                ViewBag.total = total;
            }
            return PartialView("~/Views/Partial/_CobrosPartial.cshtml", listaCobros);
        }

    }
}
