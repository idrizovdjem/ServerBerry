namespace AppRunner.ViewModels.Application
{
    using System.Diagnostics;
    
    public class ApplicationProcess
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Process Process { get; set; }
    }
}
