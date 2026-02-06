using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validador para CreateSaleCommand que define as regras de negócio para criação de vendas.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(v => v.SaleNumber).NotEmpty().Length(3, 50);
        RuleFor(v => v.CustomerId).NotEmpty();
        RuleFor(v => v.Branch).NotEmpty().Length(3, 100);
        RuleFor(v => v.Items).NotEmpty().WithMessage("A venda deve possuir ao menos um item.");

        // Validação dos itens da venda
        RuleForEach(v => v.Items).SetValidator(new CreateSaleItemCommandValidator());
    }
}

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(i => i.ProductId).NotEmpty();
        RuleFor(i => i.ProductName).NotEmpty().MaximumLength(100);
        RuleFor(i => i.UnitPrice).GreaterThan(0);

        // Regra de negócio: Quantidade máxima de 20 itens por produto
        RuleFor(i => i.Quantity)
            .NotEmpty()
            .InclusiveBetween(1, 20)
            .WithMessage("A quantidade de um item não pode ser superior a 20.");
    }
}