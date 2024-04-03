using EasyShop.IRepositories;
using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Repositories{
    public class OrderRepository:IOrderRepository{

    private readonly ApplicationDbContext dbContext;
    public OrderRepository(ApplicationDbContext context)
    {
      dbContext = context;
    }
      public Customer FindCustomerByEmail(string email)
      {
        return dbContext.Customers.FirstOrDefault(c => c.Email == email);
      }

       public Order FindPendingOrder(int customerId){
           return  dbContext.Orders
        .FirstOrDefault(o => o.CustomerId == customerId && o.OrderStatus == "Pending");

        }

        public void RemovePendingOrders(Order existingPendingOrder)
        {
          var orderItemsToRemove = dbContext.OrderItems.Where(oi => oi.OrderID == existingPendingOrder.OrderID).ToList();
          dbContext.OrderItems.RemoveRange(orderItemsToRemove);

        // Remove the existing pending order
         dbContext.Orders.Remove(existingPendingOrder);

         dbContext.SaveChanges();
        }

        public List<Cart> CartItems(int customerId){
           return  dbContext.Carts
        .Include(c => c.Product)
        .Where(c => c.CustomerId == customerId)
        .ToList();

        }

        public void AddOrder(Order order){
             dbContext.Orders.Add(order);
             dbContext.SaveChanges();
        }

        public  void AddOrderItem(OrderItem orderItem){
              dbContext.OrderItems.Add(orderItem);
              dbContext.SaveChanges();
        }
    }
}