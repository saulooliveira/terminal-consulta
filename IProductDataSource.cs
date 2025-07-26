public interface IProductDataSource
{
    Product GetProductByBarcode(string barcode);
}

public class Product
{
    public string Barcode { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
}
