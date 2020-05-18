using LM.Domain.Entities;
using LM.VUTTR.Api.Domain.Users;
using System;

namespace LM.VUTTR.Api.Domain.Tools
{
    public class UpdateUser : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        UpdateUser() { }
        internal UpdateUser(User user)
            : base(Guid.NewGuid())
        {
            User = user;
            UserId = user.Id;
        }

        public long UserId { get; private set; }

        public User User { get; private set; }
    }
}