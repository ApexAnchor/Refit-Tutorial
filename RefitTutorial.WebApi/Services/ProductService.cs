using Microsoft.EntityFrameworkCore;
using RefitTutorial.WebApi.Data;
using RefitTutorial.WebApi.Models;

namespace RefitTutorial.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext prdDbContext;

        public ProductService(ProductDbContext prdDbContext)
        {
            this.prdDbContext = prdDbContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var prd = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice
            };

            await prdDbContext.Products.AddAsync(prd);
            await prdDbContext.SaveChangesAsync();
            return prd;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var count = await prdDbContext.Products.Where(m => m.Id == productId)
                                        .ExecuteDeleteAsync();
            if (count > 0) return true;
            else return false;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await prdDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await prdDbContext.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProduct(Guid productId, Product product)
        {
            var prd = await prdDbContext.Products.FindAsync(productId);

            if (prd == null)
            {
                return null;
            }
            prd.Name = product.Name;
            prd.Description = product.Description;
            prd.Quantity = product.Quantity;
            prd.UnitPrice = product.UnitPrice;

            await prdDbContext.SaveChangesAsync();
            return prd;
        }
    }
}
