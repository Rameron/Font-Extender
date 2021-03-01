using System.Diagnostics;
using System.IO;

namespace Font_Extender
{
    public static class TTXUtils
    {
        public static void ExecuteTTXConversion(string targetPath)
        {
            var ttxConvertCommand = $"ttx {Path.GetFileName(targetPath)}";
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + ttxConvertCommand);
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.WorkingDirectory = Path.GetDirectoryName(targetPath);
            var process = Process.Start(processInfo);

            process.WaitForExit();

            process.Close();
        }

        public static string GetHistoryGlyphFileLocation(string ttxFileName)
        {
            return ttxFileName.Substring(0, ttxFileName.Length - 4) + ".history.txt";
        }
    }
}
