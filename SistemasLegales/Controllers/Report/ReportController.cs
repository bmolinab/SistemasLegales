using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SistemasLegales.Services;

namespace SistemasLegales.Controllers
{
    public class ReportController : Controller
    {

        private readonly IReporteServicio reporteServicio;
        public IConfiguration Configuration { get; }
        public ReportController( IReporteServicio reporteServicio, IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            this.reporteServicio = reporteServicio;
        }

      
        public ActionResult RepTramites(int id)
        {
            var parametersToAdd = reporteServicio.GetDefaultParameters(Configuration.GetSection("ReporteTramites").Value);
            var newUri = reporteServicio.GenerateUri(parametersToAdd);
            return Redirect(newUri);
        }

       

      


    }
}

