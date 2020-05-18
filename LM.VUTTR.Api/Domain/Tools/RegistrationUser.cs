using LM.Domain.Entities;
using LM.VUTTR.Api.Domain.Users;
using System;

namespace LM.VUTTR.Api.Domain.Tools
{
    public class RegistrationUser : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        RegistrationUser() { }
        internal RegistrationUser(User user)
            : base(Guid.NewGuid())
        {
            User = user;
            UserId = user.Id;
        }

        public long UserId { get; private set; }

        public User User { get; private set; }
    }
}