using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        // Mapeia a Entidade para o Result simplificado da listagem
        CreateMap<Sale, ListSalesResult>();
    }
}