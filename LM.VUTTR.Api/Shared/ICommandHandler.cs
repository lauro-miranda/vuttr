using LM.Responses;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Shared
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Response> HandleAsync(TCommand command);
    }
}