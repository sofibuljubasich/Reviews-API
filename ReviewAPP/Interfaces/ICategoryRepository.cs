using ReviewAPP.Models;
namespace ReviewAPP.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);   

        ICollection<Category> GetCategories();
        
        ICollection<Place> GetPlaceByCategory(int categoryId);

        bool CategoryExists(int id);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category); 
        bool DeleteCategory(Category category);
        bool Save();

    }
}
