using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa o Registro de Venda (Aggregate Root).
/// Centraliza a lógica de totalização, filial e estado da venda.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Número único da venda (pode ser um código sequencial ou amigável).
    /// </summary>
    public string SaleNumber { get; private set; } = string.Empty;

    /// <summary>
    /// Data em que a venda foi realizada.
    /// </summary>
    public DateTime SaleDate { get; private set; }

    /// <summary>
    /// Identificador do Cliente (External Identity Pattern).
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Nome ou Identificação da Filial onde a venda foi feita.
    /// </summary>
    public string Branch { get; private set; } = string.Empty;

    /// <summary>
    /// Valor total da venda somando todos os itens.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Indica se a venda foi cancelada.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Lista de produtos/itens vinculados a esta venda.
    /// </summary>
    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    private Sale() { }

    public Sale(string saleNumber, Guid customerId, string branch)
    {
        if (string.IsNullOrWhiteSpace(saleNumber))
            throw new ArgumentException("O número da venda é obrigatório.");

        if (string.IsNullOrWhiteSpace(branch))
            throw new ArgumentException("A filial é obrigatória.");

        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        Branch = branch;
        IsCancelled = false;
    }

    /// <summary>
    /// Adiciona um item à venda e atualiza o total geral.
    /// </summary>
    public void AddItem(int productId, string productName, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Não é possível adicionar itens a uma venda cancelada.");

        var item = new SaleItem(productId, productName, quantity, unitPrice);
        _items.Add(item);

        CalculateTotalAmount();
    }

    /// <summary>
    /// Cancela a venda e todos os seus itens.
    /// </summary>
    public void CancelSale()
    {
        IsCancelled = true;
        foreach (var item in _items)
        {
            item.Cancel();
        }
    }

    public void CalculateTotalAmount()
    {
        TotalAmount = _items.Sum(i => i.TotalAmount);
    }
    public void CalculateInitialTotal()
    {
        foreach (var item in _items)
        {
            item.CalculateDiscount();
        }

        TotalAmount = _items.Sum(i => i.TotalAmount);
    }
}