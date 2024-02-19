using Refit;
using RefitTutorial.Contracts.Requests;
using RefitTutorial.Contracts.Response;

namespace RefitTutorial.Sdk
{
    public interface IProductReftApi
    {
        [Get("/api/Product/GetProducts")]
        Task<GetProductsResponse> GetAllProducts();

        [Get("/api/Product/GetProductById")]
        Task<GetProductByIdResponse> GetProductById([Query]GetProductByIdRequest request);

        [Post("/api/Product/CreateProduct")]
        Task<CreateProductResponse> CreateProduct([Body]CreateProductRequest request);

        [Put("/api/Product/UpdateProduct")]
        Task<UpdateProductResponse> UpdateProduct([Body]UpdateProductRequest request);

        [Delete("/api/Product/DeleteProduct")]
        Task<DeleteProductResponse> DeleteProduct([Query]DeleteProductRequest request);

    }
}
