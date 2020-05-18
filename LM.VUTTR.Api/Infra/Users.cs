using LM.VUTTR.Api.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace LM.VUTTR.Api.Infra
{
    public class Users : IUser
    {
        public Users(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                throw new ArgumentNullException(nameof(httpContextAccessor));

            Initialize(httpContextAccessor);
        }

        public Guid? Code { get; set; }

        public string Email { get; set; }

        public bool HasUser => !Guid.Empty.Equals(Code);

        void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null)
                return;

            var user = httpContextAccessor.HttpContext.User;

            if (user != null && user.Claims != null && user.Claims.Any())
            {
                var code = user.Claims.FirstOrDefault(c => c.Type.Equals("Code"));

                var email = user.Claims.FirstOrDefault(c => c.Type.Equals("Email"));

                Code = code.Value != null ? Guid.Parse(code.Value) : default(Guid?);
                Email = email != null ? email.Value : "";
            }
        }
    }
}