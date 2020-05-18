using LM.Infra.Repositories;
using LM.Responses;
using LM.VUTTR.Api.Domain.Tools;
using LM.VUTTR.Api.Domain.Tools.Repository.Contracts;
using LM.VUTTR.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Infra.Repositories
{
    public class ToolRepository : Repository<Tool, VUTTRContext>, IToolRepository
    {
        public ToolRepository(VUTTRContext context) : base(context) { }

        public override async Task<Maybe<Tool>> FindAsync(Guid code)
            => await DbSet.Include(x => x.Tags).SingleOrDefaultAsync(tag => tag.Code.Equals(code));
    }
}