using System;

namespace LM.VUTTR.Api.Dtos.Tools
{
    public class GetToolDto
    {
        public Guid Code { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string TagName { get; set; }
    }
}