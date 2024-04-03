
using EasyShop.IRepositories;
using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AuthRepository(ApplicationDbContext context)
        {
           dbContext = context;
        }
        public bool emailExists(string email)
        {
           if(dbContext.Customers.Any(x=>x.Email==email))
           return true;
           return false;
        }

        public bool phoneNumberExists(string phoneNumber)
        {
           if(dbContext.Customers.Any(u => u.PhoneNumber == phoneNumber))
           return true;
           return false;
        }

        public void addNewCustomer(Customer customer){
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

         public bool userExists(LoginModel model)
        {
           if(dbContext.Customers.Any(u => u.Email == model.Email && u.Password==model.Password))
           return true;
           return false;
        }
    }
}