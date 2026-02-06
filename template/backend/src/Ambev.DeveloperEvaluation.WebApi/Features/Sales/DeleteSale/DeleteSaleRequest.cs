namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Modelo de requisição para deletar uma venda.
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// Identificador único da venda a ser deletada.
    /// </summary>
    public Guid Id { get; set; }
}