﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLabs.Site.Models.Infrastructure;
using GraphLabs.DomainModel;
using GraphLabs.DomainModel.Contexts;
using GraphLabs.DomainModel.Extensions;
using GraphLabs.Site.Core.OperationContext;
using GraphLabs.Site.Models.TestPoolEntry;
using Microsoft.Practices.ObjectBuilder2;

namespace GraphLabs.Site.Models.TestPool
{
    internal sealed class TestPoolEntryModelSaver : AbstractModelSaver<TestPoolEntryModel, DomainModel.TestPoolEntry>
    {
        public TestPoolEntryModelSaver(
            IOperationContextFactory<IGraphLabsContext> operationContextFactory
            ) : base(operationContextFactory)
        {
        }

        protected override Action<DomainModel.TestPoolEntry> GetEntityInitializer(TestPoolEntryModel model, IEntityQuery query)
        {
            var entity = query.Get<DomainModel.TestPoolEntry>(model.Id);

            return g =>
            {
                g.Id = model.Id;
                g.Score = model.Score;
                g.ScoringStrategy = model.ScoringStrategy;
                g.TestQuestion = model.TestQuestion;
                g.TestPool = entity.TestPool;
            };
        }

        /// <summary> Существует ли соответствующая запись в БД? </summary>
        /// <remarks> При реализации - просто проверить ключ, в базу лазить НЕ НАДО </remarks>
        protected override bool ExistsInDatabase(TestPoolEntryModel model)
        {
            return model.Id != 0;
        }

        /// <summary> При реализации возвращает массив первичных ключей сущности </summary>
        protected override object[] GetEntityKey(TestPoolEntryModel model)
        {
            return new object[] { model.Id };
        }
    }
}
