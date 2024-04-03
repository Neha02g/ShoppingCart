using EasyShop.IRepositories;
using EasyShop.Models;

namespace EasyShop.Repositories{
    public class CategoryRepository:ICategoryRepository{
      
      private readonly ApplicationDbContext dbContext;
      public CategoryRepository(ApplicationDbContext context){
        dbContext = context;
      }

     public List<Category> getAllCategories(){
        return dbContext.Categories.ToList();
     }
    
    public Category GetCategoryById(int categoryId){
        return dbContext.Categories.FirstOrDefault(x=>x.CategoryId==categoryId);
    }

    public bool FindCategory(string name){
        if(dbContext.Categories.Any(x=>x.Name.ToLower() == name.ToLower()))
        return true;
        return false;
    }

    public  void AddCategory(string name,string description)
    {
      dbContext.Categories.Add(new Category{Name=name,Description=description});
       dbContext.SaveChanges();

    }
    
    }
}