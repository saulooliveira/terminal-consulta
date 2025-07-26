public class ParadoxProductDataSource : IProductDataSource
{
    public Product GetProductByBarcode(string barcode)
    {
        // Simulado: lógica real envolveria uso de ODBC Paradox
        return barcode == "789012" ? new Product { Barcode = "789012", Description = "Feijão", Price = "R$ 9,90" } : null;
    }
}
