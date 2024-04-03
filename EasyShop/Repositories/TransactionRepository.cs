using EasyShop.IRepositories;
using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Repositories{
    public class TransactionRepository:ITransactionRepository{
      private readonly ApplicationDbContext dbContext;
      public TransactionRepository( ApplicationDbContext context)
      {
        dbContext = context;
      }

      public Customer FindCustomerByEmail(string email){
        return dbContext.Customers.FirstOrDefault(c => c.Email == email);
      }

      public  Order FindPendingOrder(int customerId){
        return dbContext.Orders
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefault(o => o.CustomerId == customerId && o.OrderStatus == "Pending");
      }

      public void Addtransaction(Transaction transaction){
        dbContext.Transactions.Add(transaction);
        dbContext.SaveChanges();
      }

      public void SaveChange()
      {
         dbContext.SaveChanges();
      }

       public List<Cart> CartItems(int customerId){
        return dbContext.Carts .Include(c => c.Product).Where(c => c.CustomerId == customerId).ToList();
       }

       public void RemoveCartItems(List<Cart> cartItemsToRemove){
         dbContext.Carts.RemoveRange(cartItemsToRemove);
         dbContext.SaveChanges();
       }
       public decimal CalculateOrderTotal(int orderId){
         return dbContext.OrderItems
        .Where(oi => oi.OrderID == orderId)
        .Sum(oi => oi.Quantity * oi.Product.Price);
       }

       public DeliveryDetail GetAddress(int orderId){
        return  dbContext.DeliveryDetails.FirstOrDefault(x=>x.OrderID==orderId);
       }
    }
}