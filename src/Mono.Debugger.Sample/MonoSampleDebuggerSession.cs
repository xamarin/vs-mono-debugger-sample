using Mono.Debugging.Soft;
using System;
using Mono.Debugging.Client;
using EnvDTE;
using System.IO;

namespace Mono.Debugger.Sample
{
    class MonoSampleDebuggerSession : SoftDebuggerSession
    {
        private System.Diagnostics.Process process;

        protected override void OnRun(DebuggerStartInfo startInfo)
        {
            base.OnRun(startInfo);
            UseOperationThread = true;

            var exeName = GetOutputAssembly(((Debugging.VisualStudio.StartInfo)startInfo).StartupProject);
            var monoDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Mono", "bin");

            if (!Directory.Exists(monoDirectory))
                throw new InvalidOperationException("Did not find Mono installation at expected location: " + monoDirectory);

            var args = ((Debugging.VisualStudio.StartInfo)startInfo).StartArgs as SoftDebuggerListenArgs;

            process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Path.Combine(monoDirectory, "mono.exe"), string.Format("--debug --debugger-agent=transport=dt_socket,address=127.0.0.1:{0} {1}", args.DebugPort, exeName));
            process.StartInfo.WorkingDirectory = monoDirectory;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        private string GetOutputAssembly(Project startupProject)
        {
            var baseFolder = startupProject.Properties.Item("FullPath").Value.ToString();
            var outFolder = startupProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
            var assemblyName = startupProject.Properties.Item("OutputFileName").Value.ToString();

            return string.Format("\"{0}\"", Path.Combine(baseFolder, outFolder, assemblyName));
        }

        protected override void OnExit()
        {
            process.Kill();
        }
    }
}