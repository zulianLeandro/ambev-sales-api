using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Venda deve ser cancelada corretamente")]
    public void Given_ActiveSale_When_Cancelled_Then_IsCancelledShouldBeTrue()
    {
        // Arrange
        var sale = new Sale("SALE001", Guid.NewGuid(), "Filial Marília");
        sale.AddItem(1, "Produto Teste", 5, 10.0m);

        // Act
        sale.CancelSale();

        // Assert
        sale.IsCancelled.Should().BeTrue();
        foreach (var item in sale.Items)
        {
            item.IsCancelled.Should().BeTrue();
            item.TotalAmount.Should().Be(0); // Itens cancelados zeram o total
        }
    }

    [Fact(DisplayName = "Não deve permitir adicionar itens em venda cancelada")]
    public void Given_CancelledSale_When_AddingItem_Then_ShouldThrowException()
    {
        // Arrange
        var sale = new Sale("SALE001", Guid.NewGuid(), "Filial Marília");
        sale.CancelSale();

        // Act
        var act = () => sale.AddItem(2, "Outro Produto", 1, 50.0m);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Não é possível adicionar itens a uma venda cancelada.");
    }
}