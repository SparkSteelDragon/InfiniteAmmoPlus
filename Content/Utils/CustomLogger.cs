using System.IO;
using Terraria.ModLoader;

public static class CustomLogger
{
    // path to the log file
    private static string logFilePath = Path.Combine(ModLoader.ModPath, "infinite-ammo-plus_log.txt");

    public static void Log(string message)
    {
        // check if the log file exists
        if (!File.Exists(logFilePath))
        {
            // create the file if it doesn't exist
            File.Create(logFilePath).Dispose();  // .Dispose() closes the file stream immediately
        }

        // add message to the log file
        File.AppendAllText(logFilePath, message + "\n");
    }
}
