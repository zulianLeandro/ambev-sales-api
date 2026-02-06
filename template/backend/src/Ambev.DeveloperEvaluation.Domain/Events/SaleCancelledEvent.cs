using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando uma venda é cancelada.
/// </summary>
public class SaleCancelledEvent : INotification
{
    public Guid SaleId { get; }
    public DateTime CancelledAt { get; }

    public SaleCancelledEvent(Guid saleId)
    {
        SaleId = saleId;
        CancelledAt = DateTime.UtcNow;
    }
}