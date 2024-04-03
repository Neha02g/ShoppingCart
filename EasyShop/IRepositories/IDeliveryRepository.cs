
using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.IRepositories{
    public interface IDeliveryRepository{
        Customer FindCustomerByEmail(string email);

        Order FindPendingOrder(int customerId);

        DeliveryDetail ExistingDeliveryDetails(int orderId);

        void UpdateDeliveryDetails(DeliveryDetailModel model,DeliveryDetail existingDeliveryDetail);
       
       void AddNewDeliveryDetails(DeliveryDetail model);
    }
}