using LM.Domain.Helpers;
using LM.VUTTR.Api.Domain.Users;
using System;
using System.Collections.Generic;

namespace LM.VUTTR.Api.Dtos.Users
{
    public class UserLoggedInDto
    {
        public UserLoggedInDto(User user)
        {
            if (user == null)
                throw new Exception("usuário não informado.");

            Email = user.Email.Address;
            ExpireDate = DateTimeHelper.GetCurrentDate().AddDays(7);

            AddClaims(user);
        }

        public Guid Code { get; set; }

        public string Email { get; private set; }

        public string Token { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public List<ClaimDto> Claims { get; private set; } = new List<ClaimDto>();

        public void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("Token não informado.");

            Token = token;
        }

        void AddClaims(User user)
        {
            Claims.Add(new ClaimDto { Type = nameof(Code), Value = user.Code.ToString() });
            Claims.Add(new ClaimDto { Type = nameof(Email), Value = user.Email.Address });
        }

        public class ClaimDto
        {
            public string Type { get; set; }

            public string Value { get; set; }
        }
    }
}