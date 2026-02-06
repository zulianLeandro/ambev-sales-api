using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class ListSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ListSalesHandler _handler;

    public ListSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListSalesHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Deve retornar lista paginada de vendas")]
    public async Task Handle_ValidRequest_ShouldReturnPaginatedList()
    {
        var query = new ListSalesQuery { Page = 1, Size = 10 };
        var sales = new List<Sale> { new Sale("S1", Guid.NewGuid(), "B1") };
        var paginatedList = new PaginatedList<Sale>(sales, 1, 1, 10);
        var mappedResults = new List<ListSalesResult> { new ListSalesResult { SaleNumber = "S1" } };

        _saleRepository.ListAsync(1, 10, Arg.Any<CancellationToken>()).Returns(paginatedList);
        _mapper.Map<List<ListSalesResult>>(Arg.Any<IEnumerable<Sale>>()).Returns(mappedResults);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(1);
    }
}