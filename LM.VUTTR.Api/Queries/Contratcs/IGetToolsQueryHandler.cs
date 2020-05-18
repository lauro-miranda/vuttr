using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Shared;
using System.Collections.Generic;

namespace LM.VUTTR.Api.Queries.Contratcs
{
    public interface IGetToolsQueryHandler : IQueryHandler<GetToolsQuery, List<GetToolsDto>> { }
}