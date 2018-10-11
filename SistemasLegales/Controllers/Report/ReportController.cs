using Microsoft.AspNetCore.Mvc;
using SistemasLegales.Services;

namespace SistemasLegales.Controllers
{
    public class ReportController : Controller
    {

        private readonly IReporteServicio reporteServicio;

        public ReportController( IReporteServicio reporteServicio)
        {
            
            this.reporteServicio = reporteServicio;
        }

      
        public ActionResult RepTramites(int id)
        {
            var parametersToAdd = reporteServicio.GetDefaultParameters("/ReportesSistemaLegalIA/ReporteTramites");
            var newUri = reporteServicio.GenerateUri(parametersToAdd);
            return Redirect(newUri);
        }

       

      


    }
}

