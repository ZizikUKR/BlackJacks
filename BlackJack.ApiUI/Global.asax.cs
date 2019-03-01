using BlackJack.ApiUI.Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BlackJack.ApiUI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfigUI.ConfigureContainer();

            GlobalConfiguration.Configuration
                        .Formatters
                        .JsonFormatter
                        .SerializerSettings
                        .ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration
                        .Formatters
                        .JsonFormatter
                        .SerializerSettings
                        .NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
