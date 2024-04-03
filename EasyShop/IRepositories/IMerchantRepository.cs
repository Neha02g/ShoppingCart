using EasyShop.Models;

namespace EasyShop.Repositories{
    public interface IMerchantRepository{
       List<Product> GetAllProducts();

       Product GetProductById(int productId);

       Category GetCategoryByName(string name);

       void AddProduct(Product product);

       void SaveChange();
    }
}