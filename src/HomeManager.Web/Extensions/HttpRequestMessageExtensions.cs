using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Dependencies;
using HomeManager.Domain.Services.Membership;

namespace HomeManager.Web.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        internal static IMembershipService GetMembershipService(this HttpRequestMessage request)
        {

            return request.GetService<IMembershipService>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {

            IDependencyScope dependencyScope = request.GetDependencyScope();
            TService service = (TService)dependencyScope.GetService(typeof(TService));

            return service;
        }
    }
}