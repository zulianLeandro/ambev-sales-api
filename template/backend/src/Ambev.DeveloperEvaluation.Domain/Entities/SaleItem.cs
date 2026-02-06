using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    private SaleItem() { }

    public SaleItem(int productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new InvalidOperationException("Não é permitido vender mais de 20 itens do mesmo produto.");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        IsCancelled = false;

        CalculateDiscount();
    }

    /// <summary>
    /// Aplica as regras de desconto baseadas na quantidade.
    /// </summary>
    public void CalculateDiscount()
    {
        if (IsCancelled) return;

        // Regras de negócio Ambev:
        // - Entre 4 e 9 itens: 10% de desconto.
        // - Entre 10 e 20 itens: 20% de desconto.
        // - Menos de 4 itens: Sem desconto.

        if (Quantity >= 10 && Quantity <= 20)
            Discount = (UnitPrice * Quantity) * 0.20m;
        else if (Quantity >= 4)
            Discount = (UnitPrice * Quantity) * 0.10m;
        else
            Discount = 0;

        TotalAmount = (UnitPrice * Quantity) - Discount;
    }

    public void Cancel()
    {
        IsCancelled = true;
        Discount = 0;
        TotalAmount = 0;
    }
}