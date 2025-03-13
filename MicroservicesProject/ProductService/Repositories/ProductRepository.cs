using Microsoft.EntityFrameworkCore;
using Shared.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ProductDbContext _productContext;

        public ProductRepository(ProductDbContext context) : base(context)
        {
            _productContext = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productContext.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _productContext.Categories.ToListAsync();
        }
        
        
        
        // two new implementations:
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _productContext.Categories
                .FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _productContext.Categories.AddAsync(category);
            await _productContext.SaveChangesAsync();
            return category;
        }
    }
}