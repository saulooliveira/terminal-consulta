using System;
using System.IO;

public static class Logger
{
    private static readonly string LogDir = "logs";

    public static void LogError(string message) => Log("ERROR", message);
    public static void LogInfo(string message) => Log("INFO", message);
    public static void LogWarning(string message) => Log("WARNING", message);

    private static void Log(string level, string message)
    {
        var path = Path.Combine(LogDir, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        Directory.CreateDirectory(LogDir);
        File.AppendAllText(path, $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}\n");
    }
}
