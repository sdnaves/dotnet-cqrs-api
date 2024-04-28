using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Infra.Data.Contexts;

namespace Sample.Infra.Data.Repositories
{

    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(SampleContext db) : base(db)
        {
        }
    }
}
