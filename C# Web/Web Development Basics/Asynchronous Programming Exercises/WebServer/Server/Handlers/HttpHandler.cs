﻿namespace WebServer.Server.Handlers
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Exceptions;
    using global::WebServer.Application.Views;
    using global::WebServer.Server.Enums;
    using global::WebServer.Server.Http.Response;
    using Handlers.Contracts;
    using Http.Contracts;
    using Routing.Contracts;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            foreach (KeyValuePair<string, IRoutingContext> kvp in this.serverRouteConfig.Routes[httpContext.HttpRequest.RequestMethod])
            {
                string pattern = kvp.Key;
                Regex regex = new Regex(pattern);
                Match match = regex.Match(httpContext.HttpRequest.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (string parameter in kvp.Value.Parameters)
                {
                    httpContext.HttpRequest.AddUrlParameters(parameter, match.Groups[parameter].Value);
                }
                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            throw new BadRequestException("Can't handle response!");
        }
    }
}
