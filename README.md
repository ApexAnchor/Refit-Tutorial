# Product API SDK with Refit Library

Refit is a strongly typed HTTP client library for .NET which turns your REST API into a live interface. We are going to use it to build an SDK to perform CRUD (Create, Read, Update, Delete) operations on a Product API.

Let's start by creating our models:

```csharp
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice
    {
        get
        {
            return UnitPrice * Quantity;
        }
    }
}
```

Then, we define a Refit interface:

```csharp
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
```

With that, the API calls are ready. Here's how you can make a call:

```csharp
var client = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7050"),
    DefaultRequestHeaders = { { "User-Agent", "My HttpClient" } },
    Timeout = TimeSpan.FromSeconds(50)
};
var refitClient = RestService.For<IProductReftApi>(client);
var getAllProductsresponse = await refitClient.GetAllProducts();
```

That's it! You just created an SDK to communicate with your Product API. Below is the details of CRUD operations:

- `GetProductById(int id)` retrieves the details of a product specified by the id.
- `GetProducts()` retrieves the list of all products.
- `CreateProduct(Product product)` creates a new product with the details supplied in `Product` object.
- `UpdateProduct(int id, Product product)` updates the product with the given id using the details supplied in `Product` object.
- `DeleteProduct(int id)` deletes the product specified by the id.

Remember, you will need to install the Refit NuGet package before you start. Here's the command to do that in the NuGet Package Manager Console:

```
Install-Package refit
```

Happy coding!
