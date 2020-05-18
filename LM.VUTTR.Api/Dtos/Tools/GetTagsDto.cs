using System.Collections.Generic;

namespace LM.VUTTR.Api.Dtos.Tools
{
    public class GetTagsDto
    {
        public List<string> Tags { get; set; } = new List<string>();
    }
}