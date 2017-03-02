﻿using GraphLabs.DomainModel;
using GraphLabs.Site.Models.AvailableLab;
using GraphLabs.Site.Models.CreateLab;
using GraphLabs.Site.Models.Groups;
using GraphLabs.Site.Models.Infrastructure;
using GraphLabs.Site.Models.Lab;
using GraphLabs.Site.Models.LabExecution;
using GraphLabs.Site.Models.News;
using GraphLabs.Site.Models.Results;
using GraphLabs.Site.Models.ResultsWithTaskInfo;
using GraphLabs.Site.Models.Schedule;
using GraphLabs.Site.Models.Schedule.Edit;
using GraphLabs.Site.Models.StudentActions;
using GraphLabs.Site.Models.TaskResults;
using GraphLabs.Site.Models.TaskResultsWithActions;
using GraphLabs.Site.Utils.IoC;
using Microsoft.Practices.Unity;

namespace GraphLabs.Site.Models.IoC
{
    /// <summary> Конфигуратор моделей </summary>
    public class ModelsConfiguration : IUnityRegistry
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IListModelLoader, ListModelLoader>(new HierarchicalLifetimeManager());

            // группы
            container.RegisterType<IEntityBasedModelSaver<GroupModel, Group>, GroupModelSaver>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<GroupModel, Group>, GroupModelLoader>(new PerResolveLifetimeManager());

            // новости
            container.RegisterType<IEntityBasedModelSaver<NewsModel, DomainModel.News>, NewsModelSaver>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<NewsModel, DomainModel.News>, NewsModelLoader>(new PerResolveLifetimeManager());

            // результаты
            container.RegisterType<IEntityBasedModelLoader<TaskResultWithActionsModel, TaskResult>, TaskResultWithActionsModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<ResultWithTaskInfoModel, Result>, ResultWithTaskInfoModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<ResultModel, Result>, ResultModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<StudentActionModel, StudentAction>, StudentActionModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<TaskResultModel, TaskResult>, TaskResultModelLoader>(new PerResolveLifetimeManager());

            // лабы
            container.RegisterType<IEntityBasedModelLoader<LabModel, LabWork>, LabModelLoader>(new PerResolveLifetimeManager());

            // доступные лабы
            container.RegisterType<IEntityBasedModelLoader<DemoLabModel, AbstractLabSchedule>, DemoLabModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<TestingLabModel, AbstractLabSchedule>, TestingLabModelLoader>(new PerResolveLifetimeManager());

            // создание лабы
            container.RegisterType<IEntityBasedModelLoader<CreateLabModel, LabWork>, CreateLabModelLoader>(new PerResolveLifetimeManager());

            // выполнение лабы
            container.RegisterType<IDemoVariantModelLoader, DemoVariantModelLoader>(new HierarchicalLifetimeManager());

            // расписание
            container.RegisterType<IEntityBasedModelLoader<LabScheduleModel, AbstractLabSchedule>, LabScheduleModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEditLabScheduleModelLoader, EditLabScheduleModelLoader>(new PerResolveLifetimeManager());
            container.RegisterType<IEntityBasedModelLoader<EditLabScheduleModelBase, AbstractLabSchedule>>(new InjectionFactory(c => c.Resolve<IEditLabScheduleModelLoader>()));


            container.RegisterType<IEntityBasedModelSaver<EditLabScheduleModelBase, AbstractLabSchedule>, EditLabScheduleModelSaver>(new PerResolveLifetimeManager());
        }
    }
}
