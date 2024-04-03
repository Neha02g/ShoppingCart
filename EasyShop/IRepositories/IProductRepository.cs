using EasyShop.Models;

namespace EasyShop.IRepositories{
    public interface IProductRepository{
         List<Product> GetAllProducts();

         List<Product> GetAllProductsBySearch(string query);
    }
}