using EnvDTE;
using Mono.Debugging.Soft;
using Mono.Debugging.VisualStudio;

namespace Mono.Debugger.Sample
{
    internal class MonoSampleStartInfo : StartInfo
    {
        public MonoSampleStartInfo(SoftDebuggerStartArgs args, DebuggingOptions options, Project startupProject) : base(args, options, startupProject)
        {
        }
    }
}