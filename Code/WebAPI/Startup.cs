using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using System.Reflection;
using System.Globalization;
using Model.DTO;
using System.Threading;

[assembly: OwinStartup(typeof(WebAPI.Startup))]

namespace WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            Bootstrapper.Run(config);
            WebApiConfig.Register(config);
            
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            app.UseWebApi(config);


            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = SettingDTO.DateFormat;
            newCulture.DateTimeFormat.ShortTimePattern = "hh:mm:ss";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy hh:mm:ss";
            newCulture.DateTimeFormat.LongDatePattern = "dd/MM/yyyy";

            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }
    }
}