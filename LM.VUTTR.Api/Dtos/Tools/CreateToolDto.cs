using System.Collections.Generic;

namespace LM.VUTTR.Api.Dtos.Tools
{
    public class CreateToolDto
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}