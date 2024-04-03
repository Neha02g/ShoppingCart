using EasyShop.IRepositories;
using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.Repositories{
    public class ProductRepository:IProductRepository{
       private readonly ApplicationDbContext dbContext;

       public ProductRepository(ApplicationDbContext context) 
       {
        dbContext =context;
       }

       public List<Product> GetAllProducts(){
        return dbContext.Products.ToList();
       }
       
        public List<Product> GetAllProductsBySearch(string query){
        return dbContext.Products.Include(p => p.Category)
                .Where(p => p.ProductName.ToLower().Contains(query) || 
                    p.Description.ToLower().Contains(query) ||
                    p.Category.Name.ToLower().Contains(query)) // Search by category name
                 .ToList();
       }
    }
}