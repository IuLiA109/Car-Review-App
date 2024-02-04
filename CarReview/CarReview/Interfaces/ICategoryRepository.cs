using CarReview.Models;

namespace CarReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Car> GetCarByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}
