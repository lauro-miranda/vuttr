using LM.VUTTR.Api.Domain.Users;
using System;

namespace LM.VUTTR.Api.Dtos.Users
{
    public class UserCreatedDto
    {
        public UserCreatedDto(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Code = user.Code;
            Email = user.Email.Address;
            CreatedAt = user.CreatedAt;
        }

        public Guid Code { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}