using Refit;
using RefitTutorial.Contracts.Requests;

namespace RefitTutorial.Sdk.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7050"),
                DefaultRequestHeaders = { { "User-Agent", "My HttpClient" } },
                Timeout = TimeSpan.FromSeconds(50)
            };

            var refitClient = RestService.For<IProductReftApi>(client);

            var getAllProductsresponse = await refitClient.GetAllProducts();

            if (getAllProductsresponse != null && getAllProductsresponse.Products.Any())
            {
                foreach (var product in getAllProductsresponse.Products)
                {
                    Console.WriteLine(product.Name);
                    Console.WriteLine(product.Description);
                    Console.WriteLine(product.UnitPrice);
                    Console.WriteLine(product.Quantity);
                    Console.WriteLine(product.TotalPrice);
                    Console.WriteLine("\n");
                }
            }


            var createProductResponse = await refitClient.CreateProduct(new CreateProductRequest()
            { Name = "MousePad", Description = "Mousepad", Quantity = 10, UnitPrice = 50 });

            Guid guid = new Guid();

            if (createProductResponse != null && createProductResponse.ProductItem != null)
            {
                guid = createProductResponse.ProductItem.Id;
                
                Console.WriteLine(createProductResponse.ProductItem.Name);
                Console.WriteLine(createProductResponse.ProductItem.Description);
                Console.WriteLine(createProductResponse.ProductItem.UnitPrice);
                Console.WriteLine(createProductResponse.ProductItem.Quantity);
                Console.WriteLine(createProductResponse.ProductItem.TotalPrice);
                Console.WriteLine("\n");
            }

            var updateProductResponse = await refitClient.UpdateProduct(new UpdateProductRequest()
            {
                Id = guid,
                Name = "MousePad",
                Description = "MousePad",
                UnitPrice = 56,
                Quantity = 45
            });


            if (updateProductResponse != null && updateProductResponse.ProductItem != null)
            {
                Console.WriteLine(updateProductResponse.ProductItem.Name);
                Console.WriteLine(updateProductResponse.ProductItem.Description);
                Console.WriteLine(updateProductResponse.ProductItem.UnitPrice);
                Console.WriteLine(updateProductResponse.ProductItem.Quantity);
                Console.WriteLine(updateProductResponse.ProductItem.TotalPrice);
                Console.WriteLine("\n");
            }

            var getProductByIdResponse = await refitClient.GetProductById(new GetProductByIdRequest() { ProductId = guid});

            if (getProductByIdResponse != null && getProductByIdResponse.ProductItem!=null)
            {  
                    Console.WriteLine(getProductByIdResponse.ProductItem.Name);
                    Console.WriteLine(getProductByIdResponse.ProductItem.Description);
                    Console.WriteLine(getProductByIdResponse.ProductItem.UnitPrice);
                    Console.WriteLine(getProductByIdResponse.ProductItem.Quantity);
                    Console.WriteLine(getProductByIdResponse.ProductItem.TotalPrice);
                    Console.WriteLine("\n");                
            }

            var deleteProductResponse = await refitClient.DeleteProduct(new DeleteProductRequest() { ProductId = guid });

            if (deleteProductResponse != null )
            {
                Console.WriteLine(deleteProductResponse.Message);              
            }
                       
            Console.ReadLine();

        }
    }
}
