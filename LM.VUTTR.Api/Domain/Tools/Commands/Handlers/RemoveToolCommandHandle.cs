using LM.Domain.UnitOfWork;
using LM.Responses;
using LM.Responses.Extensions;
using LM.VUTTR.Api.Domain.Tools.Commands.Handlers.Contracts;
using LM.VUTTR.Api.Domain.Tools.Repository.Contracts;
using LM.VUTTR.Api.Domain.Users.Repository.Contracts;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Domain.Tools.Commands.Handlers
{
    public class RemoveToolCommandHandle : IRemoveToolCommandHandle
    {
        IUser User { get; }

        IUserRepository UserRepository { get; }

        IToolRepository ToolRepository { get; }

        IUnitOfWork Uow { get; }

        public RemoveToolCommandHandle(IUser user
            , IUserRepository userRepository
            , IToolRepository toolRepository
            , IUnitOfWork uow)
        {
            User = user;
            UserRepository = userRepository;
            ToolRepository = toolRepository;
            Uow = uow;
        }

        public async Task<Response> HandleAsync(RemoveToolCommand command)
        {
            var response = Response.Create();

            if (!User.HasUser)
                return response.WithBusinessError(nameof(User), "usuário sem permissão para acessar esse recurso.");

            var currentUser = await UserRepository.FindAsync(User.Code.Value);

            if (!currentUser.HasValue)
                return response.WithBusinessError(nameof(User.Code), $"Usuário '{User.Code}' não encontrado no banco de dados.");

            var tool = await ToolRepository.FindAsync(command.Code);

            if (!tool.HasValue)
                return response.WithBusinessError(nameof(command.Code), $"Ferramenta '{User.Code}' não encontrada no banco de dados.");

            response.WithMessages(tool.Value.Delete(new UpdateUser(currentUser)).Messages);

            if (response.HasError)
                return response;

            await ToolRepository.UpdateAsync(tool);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Falha ao tentar atualizar a ferramenta.");

            return response;
        }
    }
}