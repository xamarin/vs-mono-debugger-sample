using System;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Mono.Debugging.VisualStudio;
using EnvDTE;
using Mono.Debugging.Soft;
using System.Net;

namespace Mono.Debugger.Sample
{
    internal class MonoSampleDebuggableConfig : IVsDebuggableProjectCfg, IVsProjectFlavorCfg
    {
        private IVsProjectFlavorCfg baseProjectCfg;
        private Project baseProject;

        public MonoSampleDebuggableConfig(IVsProjectFlavorCfg pBaseProjectCfg, EnvDTE.Project project)
        {
            this.baseProject = project;
            this.baseProjectCfg = pBaseProjectCfg;
        }

        public int Close()
        {
            if (baseProjectCfg != null)
            {
                baseProjectCfg.Close();
                baseProjectCfg = null;
            }

            return VSConstants.S_OK;
        }

        public int DebugLaunch(uint grfLaunch)
        {//if launch with debug; else just run
            var random = new Random(DateTime.Now.Millisecond);
            var port = 8800 + random.Next(0, 100);

            var startArgs = new SoftDebuggerListenArgs(baseProject.Name, IPAddress.Loopback, port);

            var startInfo = new MonoSampleStartInfo(startArgs, null, baseProject);
            var session = new MonoSampleDebuggerSession();

            var launcher = new MonoDebuggerLauncher(new Progress<string>());

            launcher.StartSession(startInfo, session);

            return VSConstants.S_OK;
        }

        public int QueryDebugLaunch(uint grfLaunch, out int pfCanLaunch)
        {
            pfCanLaunch = 1;
            return VSConstants.S_OK;
        }

        public int get_CfgType(ref Guid iidCfg, out IntPtr ppCfg)
        {
            ppCfg = IntPtr.Zero;

            try
            {
                if (iidCfg == typeof(IVsDebuggableProjectCfg).GUID)
                    ppCfg = Marshal.GetComInterfaceForObject(this, typeof(IVsDebuggableProjectCfg));
                else if ((ppCfg == IntPtr.Zero) && (this.baseProjectCfg != null))
                    return this.baseProjectCfg.get_CfgType(ref iidCfg, out ppCfg);
            }
            catch (InvalidCastException)
            {
            }

            return VSConstants.S_OK;
        }

        public int EnumOutputs(out IVsEnumOutputs ppIVsEnumOutputs)
        {
            throw new NotImplementedException();
        }

        public int get_BuildableProjectCfg(out IVsBuildableProjectCfg ppIVsBuildableProjectCfg)
        {
            throw new NotImplementedException();
        }

        public int get_CanonicalName(out string pbstrCanonicalName)
        {
            throw new NotImplementedException();
        }

        public int get_DisplayName(out string pbstrDisplayName)
        {
            throw new NotImplementedException();
        }

        public int get_IsDebugOnly(out int pfIsDebugOnly)
        {
            throw new NotImplementedException();
        }

        public int get_IsPackaged(out int pfIsPackaged)
        {
            throw new NotImplementedException();
        }

        public int get_IsReleaseOnly(out int pfIsReleaseOnly)
        {
            throw new NotImplementedException();
        }

        public int get_IsSpecifyingOutputSupported(out int pfIsSpecifyingOutputSupported)
        {
            throw new NotImplementedException();
        }

        public int get_Platform(out Guid pguidPlatform)
        {
            throw new NotImplementedException();
        }

        public int get_ProjectCfgProvider(out IVsProjectCfgProvider ppIVsProjectCfgProvider)
        {
            throw new NotImplementedException();
        }

        public int get_RootURL(out string pbstrRootURL)
        {
            throw new NotImplementedException();
        }

        public int get_TargetCodePage(out uint puiTargetCodePage)
        {
            throw new NotImplementedException();
        }

        public int get_UpdateSequenceNumber(ULARGE_INTEGER[] puliUSN)
        {
            throw new NotImplementedException();
        }

        public int OpenOutput(string szOutputCanonicalName, out IVsOutput ppIVsOutput)
        {
            throw new NotImplementedException();
        }

    }
}