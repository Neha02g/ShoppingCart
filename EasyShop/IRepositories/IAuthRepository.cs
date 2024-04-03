using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.IRepositories
{
public interface IAuthRepository{

  bool emailExists(string email);
 bool phoneNumberExists(string phoneNumber);

  bool userExists(LoginModel model);

 void addNewCustomer(Customer customer);
}
}