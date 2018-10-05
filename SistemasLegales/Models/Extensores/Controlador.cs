using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Models.Extensores
{
    public static class Controlador
    {
        /// <summary>
        /// Redirecciona a una Vista en el mismo Controlador.
        /// </summary>
        /// <param name="controlador">Controlador actual.</param>
        /// <param name="msg">Mensaje que saldrá en la parte superior derecha de la pantalla cuando cargue la Vista a la que se redirecciona.</param>
        /// <param name="nombreVista">Nombre de la Vista a la que se va a redireccionar, por defecto es a Index.</param>
        /// <param name="routeValues">Valores de ruta.</param>
        /// <returns></returns>
        public static IActionResult Redireccionar(this Controller controlador, string msg = null, string nombreVista = "Index", object routeValues = null)
        {
            if (!String.IsNullOrEmpty(msg))
                controlador.TempData["Mensaje"] = msg;

            return routeValues == null ? controlador.RedirectToAction(nombreVista) : controlador.RedirectToAction(nombreVista, routeValues);
        }

        public static IActionResult VistaError(this Controller controlador, object modelo, string msgError, Action accion = null)
        {
            controlador.TempData["Mensaje"] = msgError;
            accion?.Invoke();
            return controlador.View(modelo);
        }
    }
}
