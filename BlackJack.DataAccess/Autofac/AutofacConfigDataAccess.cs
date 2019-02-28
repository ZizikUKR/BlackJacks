using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BlackJack.DataAccess.Repositories.DapperRepositories;
using BlackJack.DataAccess.Repositories.EntityFrameworkRepositories;
using BlackJack.DataAccess.Repositories.Interfaces;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace BlackJack.DataAccess.Autofac
{
    public class AutofacConfigDataAccess
    {
        public static void ConfigureContainer(ContainerBuilder builder, HttpConfiguration config)
        {
             RegisterDapperRepositories(ref builder);
            //RegisterEFRepositories(ref builder);
             var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterEFRepositories(ref ContainerBuilder builder)
        {
            builder.RegisterType<EFPlayerRepository>().As<IPlayerRepository>();
            builder.RegisterType<EFGameRepository>().As<IGameRepository>();
            builder.RegisterType<EFMoveRepository>().As<IMoveRepository>();
            builder.RegisterType<EFGameResultsRepository>().As<IPlayerGameStatusRepository>();
            builder.RegisterType<BlackJackContext>().AsSelf().InstancePerRequest();
        }

        private static void RegisterDapperRepositories(ref ContainerBuilder builder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BlackJackContext"].ConnectionString.ToString();

            builder.RegisterType<DapperPlayerRepository>().As<IPlayerRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<DapperGameRepository>().As<IGameRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<DapperMoveRepository>().As<IMoveRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<DapperGameResultRepository>().As<IPlayerGameStatusRepository>().WithParameter("connectionString", connectionString);
        }
    }
}
