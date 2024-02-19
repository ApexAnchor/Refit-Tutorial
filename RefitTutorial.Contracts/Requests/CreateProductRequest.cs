
namespace RefitTutorial.Contracts.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
