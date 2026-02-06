using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Deve retornar a venda quando o ID existe")]
    public async Task Handle_ExistingId_ShouldReturnResult()
    {
        var saleId = Guid.NewGuid();
        var query = new GetSaleQuery(saleId);
        var sale = new Sale("SALE001", Guid.NewGuid(), "Branch A");
        var expectedResult = new GetSaleResult { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Deve lançar exceção quando a venda não existe")]
    public async Task Handle_NonExistingId_ShouldThrowKeyNotFoundException()
    {
        var query = new GetSaleQuery(Guid.NewGuid());
        _saleRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns((Sale)null!);

        var act = () => _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}