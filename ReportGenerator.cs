using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public static class ReportGenerator
{
    private static readonly List<Product> _products = new();
    private static readonly string ReportDir = "reports";

    public static void AddToReport(Product p)
    {
        _products.Add(p);
    }

    public static void StartScheduler()
    {
        new Thread(() =>
        {
            while (true)
            {
                if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 59)
                {
                    GenerateAndSendDailyReport();
                    Thread.Sleep(60000); // aguarda 1 minuto para evitar múltiplos envios
                }
                Thread.Sleep(10000);
            }
        }) { IsBackground = true }.Start();
    }

    private static void GenerateAndSendDailyReport()
    {
        Directory.CreateDirectory(ReportDir);
        string path = Path.Combine(ReportDir, $"relatorio-{DateTime.Now:yyyy-MM-dd}.txt");
        var agrupado = _products.GroupBy(p => p.Barcode)
            .Select(g => $"{g.First().Description} - {g.Count()} consultas")
            .ToList();

        File.WriteAllLines(path, agrupado);
        EmailService.SendEmail("Relatório de Consultas", "Segue o relatório do dia.", path);

        Logger.LogInfo("Relatório enviado.");
        _products.Clear();
    }
}
