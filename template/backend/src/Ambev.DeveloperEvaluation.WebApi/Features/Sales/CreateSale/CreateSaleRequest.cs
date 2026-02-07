using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string SaleNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<CreateSaleItemRequest> Items { get; set; } = [];
}

public class CreateSaleItemRequest
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
