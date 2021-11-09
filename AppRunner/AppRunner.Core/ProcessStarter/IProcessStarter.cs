namespace AppRunner.Core.ProcessStarter
{
    using System.Diagnostics;

    public interface IProcessStarter
    {
        Process Start(string path);
    }
}
