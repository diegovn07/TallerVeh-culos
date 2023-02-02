using Repuestos.UI.Controllers;
using System;
using System.Web;
using System.Web.Mvc;

namespace Repuestos.UI.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Modelo.Usuario oUsuario;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                oUsuario = (Modelo.Usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is CerrarSesionController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/CerrarSesion/Loggin");
                    }
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/CerrarSesion/Loggin");
            }

        }
    }
}