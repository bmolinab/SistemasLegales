using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SistemasLegales.Services
{
    public class TimedHostedService : IHostedService
    {
        private readonly SistemasLegalesContext db;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public TimedHostedService(UserManager<ApplicationUser> userManager, SistemasLegalesContext db, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.db = db;
            this.emailSender = emailSender;
        }


        public async Task EnviarNotificacionRequisitosCincoDias()
        {
            try
            {
                var fechaActual = DateTime.Now.AddDays(NotificacionContinua.Dias).Date;
                var fechaCaducidad = DateTime.Now;
                var listadoRequisitos = await db.Requisito.Where(c => c.FechaCaducidad != null && !c.Finalizado && c.Habilitado).ToListAsync();
                foreach (var item in listadoRequisitos)
                {
                    fechaCaducidad = item.FechaCaducidad.Value.Date;
                    if (fechaActual>=fechaCaducidad)
                    {
                        await item.EnviarEmailNotificaionDias(emailSender, db);
                    }
                }

            }
            catch (Exception)
            { }
        }

        public async Task EnviarNotificacionRequisitos()
        {
            try
            {
                var listadoRequisitos = await db.Requisito.Where(c => c.FechaCaducidad != null && !c.Finalizado && c.Habilitado && !c.NotificacionEnviada && !c.NotificacionEnviadaUltima).ToListAsync();
                foreach (var item in listadoRequisitos)
                    await item.EnviarEmailNotificaion(userManager,emailSender, db);
            }
            catch (Exception)
            { }
        }
    }
}
