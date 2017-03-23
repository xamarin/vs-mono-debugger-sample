//------------------------------------------------------------------------------
// <copyright file="MonoDebuggableSamplePackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Mono.Debugging.Client;

namespace Mono.Debugger.Sample
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PackageGuidString)]
    [ProvideProjectFactory(typeof (MonoSampleProjectFactory), "Mono.Debugger.Sample", null, null, null, null, LanguageVsTemplate = "CSharp", TemplateGroupIDsVsTemplate = "Mono.Debugger.Sample")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MonoSamplePackage : Package
    {
        public const string PackageGuidString = "0610d5ad-36f3-4a90-a0ba-55736bf813ba";
        public const string MonoSampleProjectGuid = "8F3E2DF0-C35C-4265-82FC-BEA011F4A7ED";

        protected override void Initialize()
        {
            base.Initialize();

            RegisterProjectFactory(new MonoSampleProjectFactory(this));

            DebuggerLoggingService.CustomLogger = new NullLogger();
        }
    }

    class NullLogger : ICustomLogger
    {
        public string GetNewDebuggerLogFilename() => null;

        public void LogAndShowException(string message, Exception ex) { }

        public void LogError(string message, Exception ex) { }

        public void LogMessage(string messageFormat, params object[] args) { }
    }
}