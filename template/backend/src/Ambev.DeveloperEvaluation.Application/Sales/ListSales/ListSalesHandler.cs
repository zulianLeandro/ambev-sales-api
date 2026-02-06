using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesQuery, PaginatedList<ListSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public ListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListSalesResult>> Handle(ListSalesQuery request, CancellationToken cancellationToken)
    {
        var salesPaginated = await _saleRepository.ListAsync(request.Page, request.Size, cancellationToken);

        var resultItems = _mapper.Map<List<ListSalesResult>>(salesPaginated);

        return new PaginatedList<ListSalesResult>(
            resultItems,
            salesPaginated.TotalCount,
            salesPaginated.CurrentPage,
            salesPaginated.PageSize);
    }
}