using Autofac;
using Autofac.Integration.WebApi;
using BlackJack.BusinessLogic.Autofac;
using System.Reflection;
using System.Web.Http;

namespace BlackJack.ApiUI.Autofac
{
    public class AutofacConfigUI
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();


            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterWebApiModelBinderProvider();

            AutofacConfigBisinesLogic.Configuration(builder, config);
        }
    }
}