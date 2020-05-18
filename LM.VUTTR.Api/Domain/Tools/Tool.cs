using LM.Domain.Entities;
using LM.Responses;
using LM.Responses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LM.VUTTR.Api.Domain.Tools
{
    public class Tool : Entity
    {
        [Obsolete(ConstructorObsoleteMessage, true)]
        Tool() { }
        public Tool(string name, string link, string descricao, RegistrationUser registrationUser)
            : base(Guid.NewGuid())
        {
            Name = name;
            Link = link;
            Description = descricao;
            RegistrationUser = registrationUser;
            RegistrationUserId = registrationUser.Id;
        }

        public string Name { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }

        public long RegistrationUserId { get; private set; }

        public RegistrationUser RegistrationUser { get; private set; }

        public long? UpdateUserId { get; private set; }

        public UpdateUser UpdateUser { get; private set; }

        public ICollection<Tag> Tags { get; private set; } = new HashSet<Tag>();

        internal Response AddTag(string name)
        {
            var response = Response.Create();

            if (Tags.Any(tag => tag.Name.Equals(name)))
                return response.WithBusinessError(nameof(name), NameInUse.Replace("{ToolName}", Name).Replace("{TasgName}", name));

            var tag = Tag.Create(name, this);

            if (tag.HasError)
                return response.WithMessages(tag.Messages);

            Tags.Add(tag);

            return response;
        }

        internal Response UpdateTags(List<string> tags)
        {
            var response = Response.Create();

            var tagsToRemove = Tags.Where(tag => !tags.Contains(tag.Name)).ToList();

            tagsToRemove.ForEach(tag => tag.Delete());

            var tagsToAdd = tags.Where(tag => !Tags.Select(t => t.Name).Contains(tag)).ToList();

            foreach (var tagToAdd in tagsToAdd)
                response.WithMessages(AddTag(tagToAdd).Messages);

            UpdateLastUpdatedDate();

            return response;
        }

        internal Response Update(string name, string link, string description, UpdateUser updateUser)
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(name))
                response.WithBusinessError(nameof(name), NameIsRequired);

            if (string.IsNullOrWhiteSpace(link))
                response.WithBusinessError(nameof(link), LinkIsRequired);

            if (string.IsNullOrWhiteSpace(description))
                response.WithBusinessError(nameof(description), DescriptionIsRequired);

            if (updateUser == null)
                response.WithBusinessError(nameof(updateUser), UpdateUserIsRequired);

            if (response.HasError)
                return response;

            Name = name;
            Link = link;
            Description = description;
            UpdateUser = updateUser;
            UpdateUserId = updateUser.Id;

            return response;
        }

        public Response Delete(UpdateUser updateUser)
        {
            var response = Response.Create();

            if (updateUser == null)
                response.WithBusinessError(nameof(updateUser), UpdateUserIsRequired);

            if (response.HasError)
                return response;

            Delete();
            UpdateLastUpdatedDate();

            foreach (var tag in Tags)
                tag.Delete();

            return response;
        }

        public static class Factory
        {
            public static Response<Tool> Create(string name, string link, string description, RegistrationUser registrationUser)
            {
                var response = Response<Tool>.Create();

                if (string.IsNullOrWhiteSpace(name))
                    response.WithBusinessError(nameof(name), NameIsRequired);

                if (string.IsNullOrWhiteSpace(link))
                    response.WithBusinessError(nameof(link), LinkIsRequired);

                if (string.IsNullOrWhiteSpace(description))
                    response.WithBusinessError(nameof(description), DescriptionIsRequired);

                if (registrationUser == null)
                    response.WithBusinessError(nameof(registrationUser), RegistrationUserIsRequired);

                if (response.HasError)
                    return response;

                return response.SetValue(new Tool(name, link, description, registrationUser));
            }
        }

        public const string NameIsRequired = "O nome da ferramenta é obrigatório.";

        public const string LinkIsRequired = "O link da ferramenta é obrigatório.";

        public const string DescriptionIsRequired = "A descrição da ferramenta é obrigatória.";

        public const string NameInUse = "A ferramenta '{ToolName}' já possuí uma tag com o nome '{TagName}'";

        public const string RegistrationUserIsRequired = "O usuário do cadastro é obrigatória.";

        public const string UpdateUserIsRequired = "O usuário da atualização é obrigatória.";

        public static implicit operator Tool(Response<Tool> response) => response.Data.Value;

        public static implicit operator Tool(Maybe<Tool> response) => response.Value;
    }
}