

using EasyShop.Models;

namespace EasyShop.IRepositories{
    public interface IOrderRepository{
        Customer FindCustomerByEmail(string email);
        Order FindPendingOrder(int customerId);

        void RemovePendingOrders(Order existingPendingOrder);

        List<Cart> CartItems(int customerId);

        void AddOrder(Order order);

         void AddOrderItem(OrderItem orderItem);

         
    }
}