using EasyShop.Models;

namespace EasyShop.IRepositories{
    public interface ICategoryRepository{

        List<Category> getAllCategories();

        Category GetCategoryById(int categoryId);

        bool FindCategory(string name);

        void AddCategory(string name,string description);
    }

}