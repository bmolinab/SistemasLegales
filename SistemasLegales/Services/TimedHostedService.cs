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

        public TimedHostedService(SistemasLegalesContext db, IEmailSender emailSender)
        {
            this.db = db;
            this.emailSender = emailSender;
        }

        public async Task EnviarNotificacionRequisitos()
        {
            try
            {
                var listadoRequisitos = await db.Requisito.Where(c => c.FechaCaducidad != null && !c.NotificacionEnviada).ToListAsync();
                foreach (var item in listadoRequisitos)
                    await item.EnviarEmailNotificaion(emailSender, db);
            }
            catch (Exception)
            { }
        }
    }
}
