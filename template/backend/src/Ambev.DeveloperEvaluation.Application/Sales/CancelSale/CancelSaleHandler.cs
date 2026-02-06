using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    public CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        // Aqui você pode criar um CancelSaleValidator se quiser ser 100% igual ao Users
        var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        await _mediator.Publish(new SaleCancelledEvent(request.Id), cancellationToken);

        return new CancelSaleResult { Success = true, Message = "Sale cancelled successfully" };
    }
}