using System.Linq;
using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Email == email);
        }

        public void AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

          public void UpdateCustomer(Customer customer)
    {
        _dbContext.Entry(customer).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }
    }
}