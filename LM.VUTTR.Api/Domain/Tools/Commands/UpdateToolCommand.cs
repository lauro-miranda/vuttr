using LM.VUTTR.Api.Shared;
using System;
using System.Collections.Generic;

namespace LM.VUTTR.Api.Domain.Tools.Commands
{
    public class UpdateToolCommand : ICommand
    {
        public UpdateToolCommand(Guid code
            , string name
            , string link
            , string description
            , List<string> tags)
        {
            Code = code;
            Name = name;
            Link = link;
            Description = description;
            Tags = tags;
        }

        public Guid Code { get; set; }

        public string Name { get; }

        public string Link { get; }

        public string Description { get; }

        public List<string> Tags { get; } = new List<string>();
    }
}