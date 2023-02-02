using System;
using System.Web.Mvc;
using Utilidades;

namespace Repuestos.UI.Controllers
{
    public class CerrarSesionController : Controller
    {
        // GET: CerrarSesion
        public ActionResult Loggin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Loggin(string Usuario, string Contrasena)
        {
            try
            {
                Utilidades.MUsuarios App = new MUsuarios();
                var oUser = MUsuarios.Loggin(Usuario.Trim(), Contrasena.Trim());
                if (oUser == null)
                {
                    ViewBag.Error = "Usuario o Contraseña invalida";
                    return RedirectToAction("Loggin", "CerrarSesion");
                }
                else
                {
                    Session["User"] = oUser;
                    return RedirectToAction("Index", "Cliente");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Logoff()
        {
            Session["User"] = null;
            return RedirectToAction("Loggin", "CerrarSesion");
        }
    }
}