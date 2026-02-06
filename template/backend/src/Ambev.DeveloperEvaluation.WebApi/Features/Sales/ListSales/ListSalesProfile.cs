using Ambev.DeveloperEvaluation.Application.Sales.ListSales; // Namespace atualizado
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<ListSalesResult, ListSalesResponse>();
    }
}