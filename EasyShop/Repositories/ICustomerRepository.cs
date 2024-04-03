using System.Collections.Generic;
using EasyShop.Models;

namespace EasyShop.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByEmail(string email);
        void AddCustomer(Customer customer);
         void UpdateCustomer(Customer customer);
    }
}