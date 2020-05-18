using LM.Domain.Entities;
using LM.Domain.Valuables;
using LM.Responses;
using LM.Responses.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace LM.VUTTR.Api.Domain.Users
{
    public class User : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        User() { }
        public User(Email email, string password)
            : base(Guid.NewGuid())
        {
            Email = email;
            Password = EncriptyPassword(password);
        }

        public Email Email { get; private set; }

        public string Password { get; private set; }

        public string EncryptionKey { get; private set; } = Guid.NewGuid().ToString();

        public bool CanAuthenticate(string password) => EncriptyPassword(password).Equals(Password);

        public static Response<User> Create(Email email, string passowrd)
        {
            var response = Response<User>.Create();

            if (email == null)
                response.WithBusinessError(nameof(email), "E-mail não informado.");

            if (string.IsNullOrWhiteSpace(passowrd))
                response.WithBusinessError(nameof(passowrd), "Senha não informada.");

            if (response.HasError)
                return response;

            return response.SetValue(new User(email, passowrd));
        }

        string EncriptyPassword(string password)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: password,
                                salt: Encoding.UTF8.GetBytes(EncryptionKey),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static implicit operator User(Response<User> response) => response.Data.Value;

        public static implicit operator User(Maybe<User> response) => response.Value;
    }
}