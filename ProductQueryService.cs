using System.Collections.Generic;

public class ProductQueryService
{
    private readonly ConnectionManager _connection;
    private readonly List<IProductDataSource> _sources;

    public ProductQueryService(ConnectionManager connection)
    {
        _connection = connection;
        _sources = new() { new DbfProductDataSource(), new ParadoxProductDataSource() };
    }

    public Product QueryProduct(string barcode)
    {
        foreach (var src in _sources)
        {
            var product = src.GetProductByBarcode(barcode);
            if (product != null)
            {
                ReportGenerator.AddToReport(product);
                return product;
            }
        }
        return null;
    }
}
