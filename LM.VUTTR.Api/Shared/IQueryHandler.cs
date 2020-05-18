using LM.Responses;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Shared
{
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}