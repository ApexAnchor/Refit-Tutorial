using Microsoft.AspNetCore.Mvc;
using RefitTutorial.Contracts.Requests;
using RefitTutorial.Contracts.Response;
using RefitTutorial.WebApi.Models;
using RefitTutorial.WebApi.Services;

namespace RefitTutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<GetProductsResponse>> GetProducts()
        {
            var products = await productService.GetAllProducts();

            var response = new GetProductsResponse()
            {
                Products = products.Select(x => new Contracts.Product()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }
                ).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductByIdResponse>> GetProductById([FromQuery]GetProductByIdRequest request)
        {
            var product = await productService.GetProductById(request.ProductId);

            if (product == null)
            {
                return BadRequest("No Product exists with the given Id");
            }

            var response = new GetProductByIdResponse()
            {
                ProductItem = new Contracts.Product()
                {
                    Name = product.Name,
                    Id = product.Id,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    UnitPrice = product.UnitPrice
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct([FromBody]CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
            };

            var prd = await productService.CreateProduct(product);

            var response = new CreateProductResponse()
            {
                ProductItem = new Contracts.Product()
                {
                    Id = prd.Id,
                    Name = prd.Name,
                    Description = prd.Description,
                    Quantity = prd.Quantity,
                    UnitPrice = prd.UnitPrice
                }
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<UpdateProductResponse>> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
            };

            var prd = await productService.UpdateProduct(request.Id, product);

            if (prd == null)
            {
                return BadRequest("No Product exists with the given Id");
            }

            var response = new UpdateProductResponse()
            {
                ProductItem = new Contracts.Product()
                {
                    Name = prd.Name,
                    Id = prd.Id,
                    Description = prd.Description,
                    Quantity = prd.Quantity,
                    UnitPrice = prd.UnitPrice
                }
            };

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteProductResponse>> DeleteProduct([FromQuery]DeleteProductRequest request)
        {
            bool result = await productService.DeleteProduct(request.ProductId);

            if (result)
            {
                return Ok(new DeleteProductResponse()
                {
                    IsDeleted = true,
                    Message = "Successfully Deleted"
                });
            }
            else
            {
                return BadRequest(new DeleteProductResponse()
                {
                    IsDeleted = false,
                    Message = "No Product available with the given id"
                });
            }
        }
    }
}
