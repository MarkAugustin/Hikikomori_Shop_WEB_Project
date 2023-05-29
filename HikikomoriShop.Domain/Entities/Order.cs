namespace HikikomoriShop.Domain.Entities;

public class Order
{
    public string Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public int CustomerId { get; set; }
    public User Customer { get; set; }
}