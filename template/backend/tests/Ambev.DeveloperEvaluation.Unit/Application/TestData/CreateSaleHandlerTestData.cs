using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleItemCommand> ItemFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(i => i.ProductId, f => f.Random.Int(1, 1000))
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100));

    private static readonly Faker<CreateSaleCommand> CreateSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(s => s.CustomerId, f => Guid.NewGuid())
        .RuleFor(s => s.Branch, f => f.Address.City());

    public static CreateSaleCommand GenerateValidCommand(int quantity = 5)
    {
        var command = CreateSaleHandlerFaker.Generate();
        command.Items = new List<CreateSaleItemCommand>
        {
            ItemFaker.Clone().RuleFor(i => i.Quantity, quantity).Generate()
        };
        return command;
    }
}