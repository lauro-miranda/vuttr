using LM.VUTTR.Api.Shared;
using System.Collections.Generic;

namespace LM.VUTTR.Api.Domain.Tools.Commands
{
    public class CreateToolCommand : ICommand
    {
        public CreateToolCommand(string name
            , string link
            , string description
            , List<string> tags)
        {
            Name = name;
            Link = link;
            Description = description;
            Tags = tags;
        }

        public string Name { get; }

        public string Link { get; }

        public string Description { get; }

        public List<string> Tags { get; } = new List<string>();
    }
}