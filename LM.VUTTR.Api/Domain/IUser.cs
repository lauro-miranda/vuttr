using System;

namespace LM.VUTTR.Api.Domain
{
    public interface IUser
    {
        Guid? Code { get; }

        string Email { get; }

        bool HasUser { get; }
    }
}