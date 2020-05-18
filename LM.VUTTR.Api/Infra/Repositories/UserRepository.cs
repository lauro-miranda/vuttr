using LM.Domain.Valuables;
using LM.Infra.Repositories;
using LM.Responses;
using LM.VUTTR.Api.Domain.Users;
using LM.VUTTR.Api.Domain.Users.Repository.Contracts;
using LM.VUTTR.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Infra.Repositories
{
    public class UserRepository : Repository<User, VUTTRContext>, IUserRepository
    {
        public UserRepository(VUTTRContext context) : base(context) { }

        public async Task<Maybe<User>> FindAsync(Email email)
            => await DbSet.FirstOrDefaultAsync(x => x.Email.Address.Equals(email.Address));
    }
}