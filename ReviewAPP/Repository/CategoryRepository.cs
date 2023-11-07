using Microsoft.EntityFrameworkCore.Diagnostics;
using ReviewAPP.Data;
using ReviewAPP.Interfaces;
using ReviewAPP.Models;

namespace ReviewAPP.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) 
        { 
            _context = context; 
        
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();        
        }


        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Place> GetPlaceByCategory(int categoryId)
        {
            return _context.PlaceCategory.Where(p => p.CategoryID == categoryId).Select(c => c.Place).ToList();
        }

        public bool UpdateCategory(Category category) 
        {
            _context.Update(category);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true:false;
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
    }
}
