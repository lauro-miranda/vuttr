using LM.Domain.UnitOfWork;
using LM.Responses;
using LM.Responses.Extensions;
using LM.VUTTR.Api.Domain.Tools.Commands.Handlers.Contracts;
using LM.VUTTR.Api.Domain.Tools.Repository.Contracts;
using LM.VUTTR.Api.Domain.Users.Repository.Contracts;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Domain.Tools.Commands.Handlers
{
    public class CreateToolCommandHandle : ICreateToolCommandHandle
    {
        IUser User { get; }

        IUserRepository UserRepository { get; }

        IToolRepository ToolRepository { get; }

        IUnitOfWork Uow { get; }

        public CreateToolCommandHandle(IUser user
            , IUserRepository userRepository
            , IToolRepository toolRepository
            , IUnitOfWork uow)
        {
            User = user;
            UserRepository = userRepository;
            ToolRepository = toolRepository;
            Uow = uow;
        }

        public async Task<Response> HandleAsync(CreateToolCommand command)
        {
            var response = Response.Create();

            if (!User.HasUser)
                return response.WithBusinessError(nameof(User), "usuário sem permissão para acessar esse recurso.");

            var currentUser = await UserRepository.FindAsync(User.Code.Value);

            if (!currentUser.HasValue)
                return response.WithBusinessError(nameof(User.Code), $"Usuário '{User.Code}' não encontrado no banco de dados.");

            var registrationUser = new RegistrationUser(currentUser);

            var tool = Tool.Factory.Create(command.Name, command.Link, command.Description, registrationUser);

            if (tool.HasError)
                return response.WithMessages(tool.Messages);

            foreach (var tag in command.Tags)
                response.WithMessages(tool.Data.Value.AddTag(tag).Messages);

            await ToolRepository.AddAsync(tool);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Falha ao tentar gravar a ferramenta.");

            return response;
        }
    }
}