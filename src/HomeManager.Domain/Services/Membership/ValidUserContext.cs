using System.Security.Principal;

namespace HomeManager.Domain.Services.Membership
{
    public class ValidUserContext
    {
        public IPrincipal Principal { get; set; }
        public UserWithRoles User { get; set; }

        public bool IsValid()
        {
            return Principal != null;
        }
    }
}