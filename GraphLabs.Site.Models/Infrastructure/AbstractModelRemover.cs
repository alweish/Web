﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLabs.DomainModel;
using GraphLabs.DomainModel.Contexts;
using GraphLabs.DomainModel.Extensions;
using GraphLabs.DomainModel.Infrastructure;
using GraphLabs.Site.Core.OperationContext;

namespace GraphLabs.Site.Models.Infrastructure
{
    internal abstract class AbstractModelRemover<TModel, TEntity> : IEntityBasedModelRemover<TModel, TEntity>
        where TModel : IEntityBasedModel<TEntity>
        where TEntity : AbstractEntity
    {
        private readonly IOperationContextFactory<IGraphLabsContext> _operationContextFactory;

        protected AbstractModelRemover(IOperationContextFactory<IGraphLabsContext> operationContextFactory)
        {
            _operationContextFactory = operationContextFactory;
        }

        public RemovalStatus Remove(long id)
        {
            using (var operation = _operationContextFactory.Create())
            {
                var entityToDelete = operation.DataContext.Query.Get<TEntity>(id);
                if (entityToDelete == null) return RemovalStatus.ElementDoesNotExist;
                Contract.Assert(typeof (TEntity).IsAssignableFrom(typeof (TEntity)));
                try
                {
                    operation.DataContext.Factory.Delete(entityToDelete);
                    operation.Complete();
                    return RemovalStatus.Success;
                }
                catch (GraphLabsDbUpdateException e)
                {
                    if (e.HResult == -2146233088) // Это код ошибки наличия внешних ключей на данный элемент в базе данных (вроде как)
                    {
                        return RemovalStatus.SomeFKExistOnTheElement;
                    }
                    return RemovalStatus.UnknownFailure;
                }
            }
        }
    }
}
