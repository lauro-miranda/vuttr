using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;
using System;

namespace LM.VUTTR.Api.Domain.Tools
{
    public class Tag : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Tag() { }
        Tag(string name, Tool tool)
            : base(Guid.NewGuid())
        {
            Name = name;
            Tool = tool;
            ToolId = tool.Id;
        }

        public string Name { get; private set; }

        public long ToolId { get; private set; }

        public Tool Tool { get; private set; }

        public static Response<Tag> Create(string name, Tool tool)
        {
            var response = Response<Tag>.Create();

            if (string.IsNullOrWhiteSpace(name))
                response.WithBusinessError(nameof(name), NameIsRequired);

            if (tool == null)
                response.WithBusinessError(nameof(tool), ToolIsRequired);

            if (response.HasError)
                return response;

            return response.SetValue(new Tag(name, tool));
        }

        public const string NameIsRequired = "O Nome da tag é obrigatório.";

        public const string ToolIsRequired = "A ferramenta é obrigatória.";

        public static implicit operator Tag(Response<Tag> response) => response.Data.Value;
    }
}