using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;
    private readonly IMediator _mediator;
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _mediator);
    }

    [Theory]
    [InlineData(5, 0.10)]
    [InlineData(15, 0.20)]
    [InlineData(2, 0)]
    public async Task Handle_ValidCommand_ShouldApplyCorrectDiscount(int quantity, decimal expectedDiscountPercent)
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand(quantity);
        var sale = new Sale(command.SaleNumber, command.CustomerId, command.Branch);

        foreach (var commandItem in command.Items)
            sale.AddItem(commandItem.ProductId, commandItem.ProductName, commandItem.Quantity, commandItem.UnitPrice);

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var item = sale.Items.First();
        var expectedDiscount = item.UnitPrice * item.Quantity * expectedDiscountPercent;

        item.Discount.Should().Be(expectedDiscount);
        await _mediator.Received(1).Publish(Arg.Any<SaleCreatedEvent>(), Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        // Arrange: Quantidade acima do permitido (21)
        var command = CreateSaleHandlerTestData.GenerateValidCommand(21);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}