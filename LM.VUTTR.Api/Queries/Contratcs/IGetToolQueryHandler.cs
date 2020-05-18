using LM.Responses;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Shared;

namespace LM.VUTTR.Api.Queries.Contratcs
{
    public interface IGetToolQueryHandler : IQueryHandler<GetToolQuery, Maybe<GetToolDto>> { }
}