using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EnviarCorreo;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SistemasLegales.Models.Entidades;
using SistemasLegales.Models.Utiles;
using SistemasLegales.Services;

namespace SistemasLegales
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SistemasLegalesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfire(_ => _.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SistemasLegalesContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(config => {
                config.ModelBindingMessageProvider.ValueMustBeANumberAccessor = (value) => $"El valor del campo {value} es inválido.";
                config.ModelBindingMessageProvider.ValueMustNotBeNullAccessor = value => $"Debe introducir el {value}";
            });

            services.AddAuthorization(opts => {
                opts.AddPolicy("Administracion", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Administrador");
                });

                opts.AddPolicy("Gestion", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Gestor","Administrador");
                });

                opts.AddPolicy("Gerencia", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Gerencia");
                });

                opts.AddPolicy("GerenciaGestion", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Gerencia", "Gestor", "Administrador");
                });
            });

            services.AddMvc();
            
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<TimedHostedService>();
            services.AddSingleton<IUploadFileService, UploadFileService>();

            services.AddSingleton<IReporteServicio, ReporteServicio>();
            services.AddSingleton<IEncriptarServicio, EncriptarServicio>();
            

            services.AddMemoryCache();
            services.AddSession();

            EstadoRequisito.Terminado = int.Parse(Configuration.GetSection("EstadoTerminado").Value);
            EstadoRequisito.Iniciado = int.Parse(Configuration.GetSection("EstadoIniciado").Value);
            //Configuracion para restar los días
            ConstantesSemaforo.MenosDiasNotificacion = int.Parse(Configuration.GetSection("MenosDiasNotificacion").Value);
            ConstantesSemaforo.MenosDiasExigible = int.Parse(Configuration.GetSection("MenosDiasExigible").Value);




            // Configuración del correo
            MailConfig.HostUri = Configuration.GetSection("Smtp").Value;
            MailConfig.PrimaryPort = Convert.ToInt32(Configuration.GetSection("PrimaryPort").Value);
            MailConfig.SecureSocketOptions = Convert.ToInt32(Configuration.GetSection("SecureSocketOptions").Value);

            MailConfig.RequireAuthentication = Convert.ToBoolean(Configuration.GetSection("RequireAuthentication").Value);
            MailConfig.UserName = Configuration.GetSection("UsuarioCorreo").Value;
            MailConfig.Password = Configuration.GetSection("PasswordCorreo").Value;

            MailConfig.EmailFrom = Configuration.GetSection("EmailFrom").Value;
            MailConfig.NameFrom = Configuration.GetSection("NameFrom").Value;

            ConstantesCorreo.MensajeCorreoSuperior = Configuration.GetSection("MensajeCorreoSuperior").Value;
            ConstantesCorreo.DominioCorreo = Configuration.GetSection("DominioCorreo").Value;


            ///mensaje correo
            ///
            ConstantesCorreo.CabeceraNotificacion = Configuration.GetSection("CabeceraNotificacion").Value;
            ConstantesCorreo.CuerpoNotificacion = Configuration.GetSection("CuerpoNotificacion").Value;
            ConstantesCorreo.UrlNotificacion = Configuration.GetSection("UrlNotificacion").Value;
            ConstantesCorreo.FooterNotificacion = Configuration.GetSection("FooterNotificacion").Value;

            //tipos de mensaje notificación

            ConstantesCorreo.MensajeCREATE = Configuration.GetSection("TipoMensaje.CREATE").Value;
            ConstantesCorreo.MensajeTERMINADO = Configuration.GetSection("TipoMensaje.TERMINADO").Value;
            ConstantesCorreo.MensajeFINALIZADO = Configuration.GetSection("TipoMensaje.FINALIZADO").Value;
            ConstantesCorreo.MensajeAUTOMATICO = Configuration.GetSection("TipoMensaje.AUTOMATICO").Value;
            ConstantesCorreo.MensajeNOACEPTADO = Configuration.GetSection("TipoMensaje.NOACEPTADO").Value;
            ConstantesCorreo.MensajeCADUCAR = Configuration.GetSection("TipoMensaje.CADUCAR").Value;

            //Constantes de envio de notificación por email
            ConstantesTimerEnvioNotificacion.Hora = int.Parse(Configuration.GetSection("Hora").Value);
            ConstantesTimerEnvioNotificacion.Minutos = int.Parse(Configuration.GetSection("Minutos").Value);
            ConstantesTimerEnvioNotificacion.Segundos = int.Parse(Configuration.GetSection("Segundos").Value);




            ReportConfig.DefaultNetworkCredentials = Convert.ToBoolean(Configuration.GetSection("DefaultNetworkCredentials").Value);

            if (!ReportConfig.DefaultNetworkCredentials)
            {
                ReportConfig.UserName = Configuration.GetSection("UserNameReport").Value;
                ReportConfig.Password = Configuration.GetSection("PasswordReport").Value;
                ReportConfig.CustomDomain = Configuration.GetSection("CustomDomain").Value;
            }
            ReportConfig.ReportServerUrl = Configuration.GetSection("ReportServerUrl").Value;
            ReportConfig.ReportFolderPath = Configuration.GetSection("ReportFolderPath").Value;
            ReportConfig.ProjectReportUrl = Configuration.GetSection("ProjectReportUrl").Value;
            ReportConfig.CompletePath = string.Format("{0}{1}", ReportConfig.ReportServerUrl, ReportConfig.ReportFolderPath);

            NotificacionContinua.Dias =Convert.ToInt32(Configuration.GetSection("DiasNotificacionesContinuas").Value);

            services.AddSingleton<IConfiguration>(Configuration);


        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TimedHostedService timedHostedService )//, IServiceProvider serviceProvider)
        {
            var defaultCulture = new CultureInfo("es-ec");
            defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
            defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture },
                FallBackToParentCultures = false,
                FallBackToParentUICultures = false,
                RequestCultureProviders = new List<IRequestCultureProvider> { }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Principal/Error");
            }
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
            //CreateRoles(serviceProvider);
            //CreateUsers(serviceProvider);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            BackgroundJob.Enqueue(() => timedHostedService.EnviarNotificacionRequisitos() );
            RecurringJob.AddOrUpdate(() => timedHostedService.EnviarNotificacionRequisitos(), $"{ConstantesTimerEnvioNotificacion.Minutos} {ConstantesTimerEnvioNotificacion.Hora} * * *");

            BackgroundJob.Enqueue(() => timedHostedService.EnviarNotificacionRequisitosCincoDias());
            RecurringJob.AddOrUpdate(() => timedHostedService.EnviarNotificacionRequisitosCincoDias(), $"{ConstantesTimerEnvioNotificacion.Minutos} {ConstantesTimerEnvioNotificacion.Hora} * * *");
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] rolesName = new string[] { Perfiles.Administrador, Perfiles.Gerencia, Perfiles.Gestor };
            IdentityResult result;
            foreach (var item in rolesName)
            {
                var roleExist = roleManager.RoleExistsAsync(item).Result;
                if (!roleExist)
                {
                    //Se crean los roles si no existen en la BD
                    result = roleManager.CreateAsync(new IdentityRole(item)).Result;
                }
            }
        }

        private void CreateUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var usersName = new ApplicationUser[]
            {
                new ApplicationUser { UserName = "administrador@bekaert.com", Email = "administrador@bekaert.com" },
                new ApplicationUser { UserName = "gerencia@bekaert.com", Email = "gerencia@bekaert.com" },
                new ApplicationUser { UserName = "gestor@bekaert.com", Email = "gestor@bekaert.com" }
            };
            IdentityResult result;
            foreach (var item in usersName)
            {
                var user = userManager.FindByNameAsync(item.UserName).Result;
                if (user == null)
                {
                    //Se crean los usuarios si no existen en la BD
                    switch (item.UserName)
                    {
                        case "administrador@bekaert.com": result = userManager.CreateAsync(item, "Administrador2018*").Result; break;
                        case "gerencia@bekaert.com": result = userManager.CreateAsync(item, "Gerencia2018*").Result; break;
                        case "gestor@bekaert.com": result = userManager.CreateAsync(item, "Gestor2018*").Result; break;
                    }
                }

                //Se asignan los roles a los usuarios si no existen en la BD
                switch (item.UserName)
                {
                    case "administrador@bekaert.com": result = userManager.AddToRoleAsync(item, Perfiles.Administrador).Result; break;
                    case "gerencia@bekaert.com": result = userManager.AddToRoleAsync(item, Perfiles.Gerencia).Result; break;
                    case "gestor@bekaert.com": result = userManager.AddToRoleAsync(item, Perfiles.Gestor).Result; break;
                }
            }
        }
    }
}
