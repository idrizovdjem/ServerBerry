namespace AppRunner.Core
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using AppRunner.Data.Models;
    using AppRunner.Core.Loggers;
    using AppRunner.Common.Exceptions;
    using AppRunner.Core.ProcessStarter;
    using AppRunner.ViewModels.Application;

    public class AppsContainer
    {
        private readonly Application[] applications;
        private readonly List<ApplicationProcess> runningProcesses;
        private readonly IProcessStarter processStarter;

        public AppsContainer(Application[] applications)
        {
            this.applications = applications;
            this.processStarter = GetProcessStarter();
            this.runningProcesses = new List<ApplicationProcess>();
        }

        public ILogger Logger { get; init; }

        public IReadOnlyList<ApplicationProcess> RunningProcess => this.runningProcesses.AsReadOnly();

        public void StartApps()
        {
            foreach (Application appInfo in applications)
            {
                Process process = this.processStarter.Start(appInfo.Path);
                ApplicationProcess applicationProcess = new ApplicationProcess()
                {
                    Id = appInfo.Id,
                    Name = appInfo.Name,
                    Process = process
                };

                this.runningProcesses.Add(applicationProcess);
                this.Logger.Log($"{appInfo.Name} started!");

                process.Exited += new EventHandler((object sender, EventArgs e) => this.runningProcesses.Remove(applicationProcess));
            }

            this.Logger.Log(string.Empty);
        }

        public void StopApps()
        {
            foreach (ApplicationProcess applicationProcess in this.runningProcesses)
            {
                applicationProcess.Process.Kill(true);
                applicationProcess.Process.Dispose();
                this.Logger.Log($"Process {applicationProcess.Name} has been stopped");
            }

            this.runningProcesses.Clear();
        }

        private static IProcessStarter GetProcessStarter()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsProcessStarter();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LinuxProcessStarter();
            }

            throw new OSNotSupportedException();
        }
    }
}
