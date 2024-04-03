

using EasyShop.IRepositories;
using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Repositories{
    public class CartRepository:ICartRepository{

        private readonly ApplicationDbContext dbContext;
        public CartRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public Customer FindCustomerByEmail(string email){
          return dbContext.Customers.FirstOrDefault(c => c.Email == email);   
        }

        public List<Cart> CartData(int customerId){
            return dbContext.Carts.Include(c => c.Product).Where(x => x.CustomerId == customerId).ToList();
        }
        public Product FindProductById(int productId){
            return dbContext.Products.FirstOrDefault(p => p.ProductID == productId);
        }

        public Cart ItemAlreadyPresent(int customerId,int productId){
        return dbContext.Carts.FirstOrDefault(x=>x.CustomerId==customerId && x.ProductID==productId);
        }

        public void AddItemToCart(Cart cartItem){
             dbContext.Carts.Add(cartItem);
             dbContext.SaveChanges();
        }

        public  void RemoveItemFromCart(Cart cartItem){
             dbContext.Carts.Remove(cartItem);
             dbContext.SaveChanges();
        }

         public void ReduceQuantity(Cart cartItem){
            cartItem.Quantity -= 1;
            dbContext.SaveChanges();
         }

           public void IncreaseQuantity(Cart cartItem){
             cartItem.Quantity += 1;
            dbContext.SaveChanges();
         }
    }
}