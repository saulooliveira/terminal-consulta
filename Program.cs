using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Logger.LogInfo("Iniciando aplicação TC 506 E");

        ConnectionManager connection = new();
        connection.Start();

        ProductQueryService queryService = new(connection);
        ReportGenerator.StartScheduler();

        while (true)
        {
            try
            {
                var barcode = connection.ReceiveBarcode();
                if (barcode != null)
                {
                    var product = queryService.QueryProduct(barcode);
                    if (product == null)
                    {
                        connection.SendNotFound();
                        Logger.LogWarning($"Produto não encontrado: {barcode}");
                    }
                    else
                    {
                        connection.SendProduct(product);
                        Logger.LogInfo($"Consulta: {barcode} -> {product.Description}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro na consulta: " + ex.Message);
            }

            Thread.Sleep(100);
        }
    }
}
