using LM.Responses;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Shared;
using System;

namespace LM.VUTTR.Api.Queries
{
    public class GetToolQuery : IQuery<Maybe<GetToolDto>> 
    {
        public GetToolQuery(Guid code)
        {
            Code = code;
        }

        public Guid Code { get; }
    }
}