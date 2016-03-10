﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLabs.Site.Logic.Security;
using GraphLabs.Site.Logic.Tasks;
using GraphLabs.Site.Logic.XapParsing;
using GraphLabs.Site.Utils.IoC;
using GraphLabs.Tasks.Contract;
using Microsoft.Practices.Unity;

namespace GraphLabs.Site.Logic.IoC
{
    public class SiteLogicConfiguration : IUnityRegistry
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            // Сервисы для сайта
            container.RegisterType<IHashCalculator, BCryptCalculator>(new PerResolveLifetimeManager());
            container.RegisterType<IInitParamsProvider, InitParamsProvider>(new PerResolveLifetimeManager());
            container.RegisterType<IAuthenticationSavingService, FormsAuthenticationSavingService>(new PerResolveLifetimeManager());
            container.RegisterType<IMembershipEngine, MembershipEngine>(new HierarchicalLifetimeManager());
            container.RegisterType<IXapProcessor, XapParsing.XapProcessor>(new PerResolveLifetimeManager());

            // Доменные сервисы, которые из этой сборки надо убирать (логика -> в толстые модели)
            container.RegisterType<INewsManager, NewsManager>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskManager, TaskManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IResultsManager, ResultsManager>(new HierarchicalLifetimeManager());
        }
    }
}
