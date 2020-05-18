using Dapper;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Queries.Contratcs;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Queries
{
    public class GetTagsQueryHandler : IGetTagsQueryHandler
    {
        IDbConnection Connection { get; }

        public GetTagsQueryHandler(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("Me"));
        }

        public async Task<GetTagsDto> HandleAsync(GetTagsQuery query)
        {
            var tags = await Connection.QueryAsync<TagDto>("select * from Tags where ToolCode = @ToolCode", new { query.ToolCode });
            return new GetTagsDto { Tags = tags.Select(t => t.Name).ToList() };
        }

        class TagDto
        {
            public Guid ToolCode { get; set; }

            public string Name { get; set; }
        }
    }
}