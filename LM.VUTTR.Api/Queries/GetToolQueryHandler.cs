using Dapper;
using LM.Responses;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Queries.Contratcs;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Queries
{
    public class GetToolQueryHandler : IGetToolQueryHandler
    {
        IDbConnection Connection { get; }

        public GetToolQueryHandler(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("Me"));
        }

        public async Task<Maybe<GetToolDto>> HandleAsync(GetToolQuery query)
        {
            var tools = await Connection.QueryAsync<GetToolDto>("select * from Tools where Code = @Code", new { query.Code });
            return tools.FirstOrDefault();
        }
    }
}