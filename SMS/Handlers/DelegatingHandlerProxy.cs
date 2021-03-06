﻿using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Handlers
{
    public sealed class DelegatingHandlerProxy<THandler> : DelegatingHandler
    where THandler : DelegatingHandler
    {
        private readonly Container container;

        public DelegatingHandlerProxy(Container container)
        {
            this.container = container;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {

            // Important: Trigger the creation of the scope.
            request.GetDependencyScope();

            var handler = this.container.GetInstance<THandler>();

            if (!object.ReferenceEquals(handler.InnerHandler, this.InnerHandler))
            {
                handler.InnerHandler = this.InnerHandler;
            }

            var invoker = new HttpMessageInvoker(handler);

            return invoker.SendAsync(request, cancellationToken);
        }
    }
}