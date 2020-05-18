using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Shared;
using System;

namespace LM.VUTTR.Api.Queries
{
    public class GetTagsQuery : IQuery<GetTagsDto> 
    {
        public GetTagsQuery(Guid toolCode)
        {
            ToolCode = toolCode;
        }

        public Guid ToolCode { get; }
    }
}