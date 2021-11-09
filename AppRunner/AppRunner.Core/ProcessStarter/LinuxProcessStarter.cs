namespace AppRunner.Core.ProcessStarter
{
    using System.Diagnostics;

    public class LinuxProcessStarter : IProcessStarter
    {
        public Process Start(string path)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("sudo", path);
            return Process.Start(processStartInfo);
        }
    }
}
