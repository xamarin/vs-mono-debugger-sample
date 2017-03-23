using System.Runtime.InteropServices;
using System;
using Microsoft.VisualStudio.Shell.Flavor;

namespace Mono.Debugger.Sample
{
    [ComVisible(false)]
    [Guid(MonoSamplePackage.MonoSampleProjectGuid)]
    public class MonoSampleProjectFactory : FlavoredProjectFactoryBase
    {
        private MonoSamplePackage package;

        public MonoSampleProjectFactory(MonoSamplePackage package)
        {
            this.package = package;
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown)
        {
            return new MonoSampleFlavoredProject(package);
        }
    }

}