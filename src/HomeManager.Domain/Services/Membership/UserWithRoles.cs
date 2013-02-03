using System.Collections.Generic;
using HomeManager.Domain.Entities;

namespace HomeManager.Domain.Services.Membership
{
    public class UserWithRoles
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}