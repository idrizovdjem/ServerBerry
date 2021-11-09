namespace AppRunner.Core.ProcessStarter
{
    using System.Diagnostics;

    public class WindowsProcessStarter : IProcessStarter
    {
        public Process Start(string path)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(path)
            {
                Verb = "runas"
            };

            Process process = Process.Start(processStartInfo);
            return process;
        }
    }
}
