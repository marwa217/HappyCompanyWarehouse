using HappyCompany.Domain.Data;
using HappyCompany.Domain.Interface.Repos;
using HappyCompany.Domain.Models;

namespace HappyCompany.Application.Repos
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
                
        }
    }
}
