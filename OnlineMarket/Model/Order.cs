namespace OnlineMarket.Model;

public class Order
{
    public int Id { get; set; }
    //Customer uchun tashqi kalit
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    //Product uchun tashqi kalit
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int Amount { get; set; }
    public DateTime DateTime { get; set; }
}