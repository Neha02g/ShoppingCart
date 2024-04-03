using EasyShop.IRepositories;
using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.Repositories{
    public class ProfileRepository:IProfileRepository{
        private readonly ApplicationDbContext dbContext;
        public ProfileRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public Customer FindCustomerByEmail(string email)
        {
            return dbContext.Customers.FirstOrDefault(c => c.Email == email);
        }

        public bool FindCustomerByPhoneNumber(string number){
            if(dbContext.Customers.Any(x=>x.PhoneNumber==number))
            return true;
            return false;
        }

       public void UpdateCustomerData(Customer customer,ProfileDataModel profile){
            customer.FirstName = profile.FirstName;
            customer.LastName = profile.LastName;
            customer.PhoneNumber = profile.PhoneNumber;
            dbContext.SaveChanges();
       }
    }
}