using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HomeManager.Web.Extensions;
using WebApiDoodle.Web.MessageHandlers;

namespace HomeManager.Web.Handlers
{
    public class HomeManagerAuthHandler : BasicAuthenticationHandler
    {
        protected override Task<IPrincipal> AuthenticateUserAsync(HttpRequestMessage request, string username, string password, CancellationToken cancellationToken)
        {
            var membershipService = request.GetMembershipService();

            var validUserCtx = membershipService.ValidateUser(username, password);

            return Task.FromResult(validUserCtx.Principal);

        }
    }
}