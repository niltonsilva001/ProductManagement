namespace ProductManagement.Domain.Events;

public class ProductOutOfStockEvent
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}