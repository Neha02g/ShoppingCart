using EasyShop.Models;

namespace EasyShop.Repositories{
    public class MerchantRepository:IMerchantRepository{
        private readonly ApplicationDbContext dbContext;
        public MerchantRepository(ApplicationDbContext context){
           dbContext = context;
        }

        public List<Product> GetAllProducts(){
            return dbContext.Products.ToList();
        }

        public Product GetProductById(int productId){
            return dbContext.Products.FirstOrDefault(x=>x.ProductID==productId);
        }

        public  Category GetCategoryByName(string name){
           return dbContext.Categories.FirstOrDefault(x=>x.Name.ToLower() == name.ToLower());
        }

        public void AddProduct(Product product){
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

         public void SaveChange(){
            dbContext.SaveChanges();
         }
    }
}