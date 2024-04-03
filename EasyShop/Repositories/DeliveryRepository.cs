

using EasyShop.IRepositories;
using EasyShop.Models;
using EasyShop.ViewModel;

namespace EasyShop.Repositories{
    public class DeliveryRepository:IDeliveryRepository{

      public readonly ApplicationDbContext dbContext;
      public DeliveryRepository(ApplicationDbContext context)
      {
        dbContext = context;
      }
      public Customer FindCustomerByEmail(string email){
          return dbContext.Customers.FirstOrDefault(c => c.Email == email);   
        }

        public Order FindPendingOrder(int customerId){
           return dbContext.Orders
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefault(o => o.CustomerId == customerId && o.OrderStatus == "Pending");

        }

        public  DeliveryDetail ExistingDeliveryDetails(int orderId){
            return dbContext.DeliveryDetails.FirstOrDefault(dd => dd.OrderID == orderId);
        }

        public void UpdateDeliveryDetails(DeliveryDetailModel model,DeliveryDetail  existingDeliveryDetail){
            existingDeliveryDetail.FullName = model.FullName;
            existingDeliveryDetail.Address = model.Address;
            existingDeliveryDetail.Pincode = model.Pincode;
            existingDeliveryDetail.MobileNumber = model.MobileNumber;

            dbContext.SaveChanges();
        }

        public  void AddNewDeliveryDetails(DeliveryDetail model){
            dbContext.DeliveryDetails.Add(model);
           dbContext.SaveChanges();
        }
    }
}