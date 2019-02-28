using Autofac;
using Autofac.Integration.Mvc;
using BlackJack.BusinessLogic.Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace BlackJack.UI.Autofac
{
    public class AutofacConfigUI
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);

            AutofacConfigBusinessLogic.ConfigureContainer(builder, config);
        }
    }
}
