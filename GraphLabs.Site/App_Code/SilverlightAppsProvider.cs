﻿using System.IO;
using System.Linq;
using System.Web;
using GraphLabs.DomainModel.Contexts;
using GraphLabs.Site.Core;
using GraphLabs.Site.ServicesConfig;
using Microsoft.Practices.Unity;
using GraphLabs.Site.Utils.Extensions;

namespace GraphLabs.Site
{
    public class SilverlightAppsProvider : IHttpHandler
    {
        #region Реализация IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var appKind = request.QueryString["kind"];

            long taskId;
            if (!long.TryParse(request.QueryString["taskId"], out taskId))
                return;

            switch (appKind)
            {
                case "generator":                    
                    ProvideGenerator(taskId, context.Response);
                    break;
                case "task":
                    ProvideTask(taskId, context.Response);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return true; }
        }

        #endregion // Реализация IHttpHandler


        #region Вспомагательные функции

        private void ProvideGenerator(long taskId, HttpResponse response)
        {
            using (var container = IoC.GetChildContainer())
            {
                var ctx = container.Resolve<ITasksContext>();
                var system = container.Resolve<ISystemContext>();

                var task = ctx.Tasks.Find(taskId);
                if (task == null)
                    return;

                response.ContentType = "application/x-silverlight-app";
                var generator = task.VariantGenerator;
                if (generator == null)
                {
                    generator = system.Settings.Query.Single().DefaultVariantGenerator;
                    if (generator == null)
                    {
                        throw new GraphLabsException("Отсутствует генератор по-умолчанию.");
                    }
                }

                using (var writer = new BinaryWriter(response.OutputStream))
                {
                    writer.Write(generator);
                }
            }
        }

        private void ProvideTask(long taskId, HttpResponse response)
        {
            using (var container = IoC.GetChildContainer())
            {
                var ctx = container.Resolve<ITasksContext>();

                var task = ctx.Tasks.Find(taskId);
                if (task == null)
                    return;
                response.ContentType = "application/x-silverlight-app";
                using (var writer = new BinaryWriter(response.OutputStream))
                {
                    writer.Write(task.TaskData.Xap);
                }
            }
        }

        #endregion // Вспомагательные функции
    }
}