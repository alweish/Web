﻿using System;
using System.Diagnostics.Contracts;
using GraphLabs.DomainModel.Contexts;
using GraphLabs.DomainModel.EF.Services;
using GraphLabs.DomainModel.Repositories;
using JetBrains.Annotations;

namespace GraphLabs.DomainModel.EF.Repositories
{
    /// <summary> Фабрика репозиториев </summary>
    [UsedImplicitly]
    public sealed class RepositoryFactory
    {
        private readonly GraphLabsContext _context;
        private readonly ISystemDateService _systemDateService;
        private readonly ITasksContext _tasksContext;

        /// <summary> Фабрика репозиториев </summary>
        public RepositoryFactory(GraphLabsContext context, ISystemDateService systemDateService, ITasksContext tasksContext)
        {
            Contract.Requires(context != null);
            _context = context;
            _systemDateService = systemDateService;
            _tasksContext = tasksContext;
        }

        /// <summary> Получить репозиторий с группами </summary>
        [NotNull]
        public IGroupRepository GetGroupRepository()
        {
            Contract.Ensures(Contract.Result<IGroupRepository>() != null);

            return new GroupRepository(_context);
        }

        /// <summary> Получить репозиторий с группами </summary>
        [NotNull]
        public IUserRepository GetUserRepository()
        {
            Contract.Ensures(Contract.Result<IUserRepository>() != null);

            return new UserRepository(_context);
        }

        /// <summary> Получить репозиторий с группами </summary>
        [NotNull]
        public ISessionRepository GetSessionRepository()
        {
            Contract.Ensures(Contract.Result<ISessionRepository>() != null);

            return new SessionRepository(_context, _systemDateService);
        }

        /// <summary> Получить репозиторий с лабораторными работами </summary>
        [NotNull]
        public ILabRepository GetLabRepository()
        {
            Contract.Ensures(Contract.Result<ILabRepository>() != null);

            return new LabRepository(_context, _tasksContext);
        }

        /// <summary> Получить репозиторий с новостями </summary>
        [NotNull]
        public INewsRepository GetNewsRepository()
        {
            Contract.Ensures(Contract.Result<INewsRepository>() != null);

            return new NewsRepository(_context);
        }

        /// <summary> Получить репозиторий с новостями </summary>
        [NotNull]
        public IResultsRepository GetResultsRepository()
        {
            Contract.Ensures(Contract.Result<IResultsRepository>() != null);

            return new ResultsRepository(_context);
        }

		[NotNull]
		public ICategoryRepository GetCategoryRepository()
		{
			Contract.Ensures(Contract.Result<ICategoryRepository>() != null);

			return new CategoryRepository(_context);
		}

        [NotNull]
        public ISurveyRepository GetSurveyRepository()
        {
            Contract.Ensures(Contract.Result<ISurveyRepository>() != null);

            return new SurveyRepository(_context);
        }
    }
}