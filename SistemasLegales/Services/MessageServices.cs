using EnviarCorreo;
using SendMails.methods;
using SistemasLegales.Models.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

        public async Task SendEmailAsync(List<string> emailsTo, string subject, string message)
        {
            try
            {
                List<Mail> mails = new List<Mail>();
                foreach (var item in emailsTo)
                {
                    mails.Add(new Mail
                    {
                        Body = ConstantesCorreo.MensajeCorreoSuperior + message,
                        EmailTo = item,
                        NameTo = "Sistemas Legales",
                        Subject = subject
                    });
                }
                await Emails.SendEmailAsync(mails);
            }
            catch (Exception)
            { }
        }
    }
}
