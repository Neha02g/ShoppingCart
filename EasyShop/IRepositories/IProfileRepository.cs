using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.IRepositories{
    public interface IProfileRepository{
        Customer FindCustomerByEmail(string email);
        bool FindCustomerByPhoneNumber(string number);

        void UpdateCustomerData(Customer customer,ProfileDataModel profile);
    }
}