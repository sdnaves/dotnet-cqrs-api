using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Infra.Data.Contexts;

namespace Sample.Infra.Data.Repositories
{
    public class CustomerRepository(SampleContext db) : BaseRepository<Customer>(db), ICustomerRepository
    {
    }
}
