using LM.Domain.Repositories;
using LM.Domain.Valuables;
using LM.Responses;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Domain.Users.Repository.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Maybe<User>> FindAsync(Email email);
    }
}