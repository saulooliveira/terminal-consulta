public class DbfProductDataSource : IProductDataSource
{
    public Product GetProductByBarcode(string barcode)
    {
        // Simulado: lógica real envolveria leitura de arquivo DBF
        return barcode == "123456" ? new Product { Barcode = "123456", Description = "Arroz", Price = "R$ 19,90" } : null;
    }
}
