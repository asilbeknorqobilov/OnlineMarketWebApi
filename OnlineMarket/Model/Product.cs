namespace OnlineMarket.Model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
    //katalog uchun tashqi kalit
    public int CatalogId { get; set; }
    public Catalog? Catalog { get; set; }
}