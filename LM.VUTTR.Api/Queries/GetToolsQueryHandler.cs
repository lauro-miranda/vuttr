using Dapper;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Queries.Contratcs;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Queries
{
    public class GetToolsQueryHandler : IGetToolsQueryHandler
    {
        IDbConnection Connection { get; }

        public GetToolsQueryHandler(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("Me"));
        }

        public async Task<List<GetToolsDto>> HandleAsync(GetToolsQuery query)
        {
            var tools = await Connection.QueryAsync<GetToolsDto>("select * from Tools");
            return tools.ToList();
        }
    }
}