using EasyShop.Models;

namespace EasyShop.IRepositories{
    public interface ITransactionRepository{
        Customer FindCustomerByEmail(string email);

        Order FindPendingOrder(int customerId);
        void Addtransaction(Transaction transaction);

         void SaveChange();

         List<Cart> CartItems(int customerId);

         void RemoveCartItems(List<Cart> cartItemsToRemove);

         decimal CalculateOrderTotal(int orderId);
          DeliveryDetail GetAddress(int orderId);
    }
}