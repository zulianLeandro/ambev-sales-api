using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class SaleEventHandler :
    INotificationHandler<SaleCreatedEvent>,
    INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleEventHandler> _logger;

    public SaleEventHandler(ILogger<SaleEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Demonstração do diferencial solicitado no README
        _logger.LogInformation("DIFFERENTIAL: Sale {SaleNumber} created for Customer {CustomerId}. Amount: {Total}",
            notification.Sale.SaleNumber,
            notification.Sale.CustomerId,
            notification.Sale.TotalAmount);

        return Task.CompletedTask;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("DIFFERENTIAL: Sale {SaleId} was CANCELLED at {CancelledAt}",
            notification.SaleId,
            notification.CancelledAt);

        return Task.CompletedTask;
    }
}