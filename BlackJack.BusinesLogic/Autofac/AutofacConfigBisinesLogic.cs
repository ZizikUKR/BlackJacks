﻿using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Autofac;
using System.Web.Http;

namespace BlackJack.BusinessLogic.Autofac
{
    public class AutofacConfigBisinesLogic
    {
        public static void Configuration(ContainerBuilder builder, HttpConfiguration config)
        {
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<HistoryService>().As<IHistoryService>();

            AutofacConfigDataAccess.Configuration(builder, config);
        }
    }
}
