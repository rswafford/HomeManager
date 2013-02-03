using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeManager.Domain.Entities.Core;

namespace HomeManager.Domain.Entities
{
    public class UserInRole : IEntity
    {
        [Key]
        public Guid Key { get; set; }
        public Guid UserKey { get; set; }
        public Guid RoleKey { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
