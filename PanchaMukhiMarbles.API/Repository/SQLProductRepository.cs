using Microsoft.EntityFrameworkCore;
using PanchaMukhiMarbles.API.Data;
using PanchaMukhiMarbles.API.Models.Domain;

namespace PanchaMukhiMarbles.API.Repository
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly PanchaMukhiMarblesDbContext dbContext;

        public SQLProductRepository(PanchaMukhiMarblesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
          var existingProduct=await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }
            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await dbContext.Products.Include("Category").ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
           return await dbContext.Products.Include("Category").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
          var existingProduct=await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.ProductName=product.ProductName;
            existingProduct.Description=product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Price=product.Price;            
            existingProduct.MadeIn=product.MadeIn;
            existingProduct.CategoryId=product.CategoryId;
            existingProduct.Thickness=product.Thickness;
            existingProduct.CompanyName=product.CompanyName;
            existingProduct.InStock=product.InStock;

            await dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
