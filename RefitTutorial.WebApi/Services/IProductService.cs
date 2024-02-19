using RefitTutorial.WebApi.Models;

namespace RefitTutorial.WebApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(Guid id);

        Task<Product> CreateProduct(Product product);

        Task<Product> UpdateProduct(Guid productId, Product product);

        Task<bool> DeleteProduct(Guid productId);
    }
}
