using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando uma venda é criada.
/// </summary>
public class SaleCreatedEvent : INotification
{
    public Sale Sale { get; }
    public SaleCreatedEvent(Sale sale) => Sale = sale;
}

