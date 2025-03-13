using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        
        
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
    }
}