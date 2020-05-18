using LM.VUTTR.Api.Shared;
using System;

namespace LM.VUTTR.Api.Domain.Tools.Commands
{
    public class RemoveToolCommand : ICommand
    {
        public RemoveToolCommand(Guid code)
        {
            Code = code;
        }

        public Guid Code { get; set; }
    }
}