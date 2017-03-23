# Mono Debugger Visual Studio Extension Sample

This repository contains a source sample that consumes the Mono debugger in a custom 
Visual Studio project flavor (that is, new kind of project you're creating).

The project is set up and configured to built and run from Visual Studio 2015, 
ready for deploying and testing into the Visual Studio 2015 Experimental instance.

The resulting VSIX does work under VS2017 too, but in order to build and run from 
VS2017, a one-way upgrade will be performed by Visual Studio upon opening the project, 
and the `Microsoft.VSSDK.BuildTools` nuget package will be updated to the latest `15.*` 
version which can only deploy to Visual Studio 2017 Experimental instance instead.

# How It Works

The solution contains two projects:

* Mono.Debugger.Sample: the actual Visual Studio extension that hooks up the debugger
* MonoSampleProject: the source for a project template that users can "unfold" by 
  doing File | New | MonoSampleProject in Visual Studio.

The extension provides a new project template and accompanying custom project flavor. 
In order for VS to determine that our custom project needs to be initialized (and its 
containing package loaded), a .csproj must declare our custom flavor GUID as part of 
the `<ProjectTypeGuids>` element. This is provided in the 
[included project template](https://github.com/xamarin/vs-mono-debugger-sample/blob/master/src/MonoSampleProject/ProjectTemplate.csproj#L9)
for eacy testing after hitting F5 on VS to try out the solution.

This sample uses the "classic" project extensibility in Visual Studio. You can read 
more about it in the [Project Subtypes](https://msdn.microsoft.com/en-us/library/bb166488.aspx)
section of the VSSDK documentation.


# Customizing for Production

At a minimum, you should rename all `MonoSample` instances with your own project name/prefix.

Required changes before distribution, to avoid collisions with other projects based on this 
sample, are:

* `MonoSamplePackage.PackageGuidString`: this is the extension package GUID, and should be your 
  own unique value
* `MonoSamplePackage.MonoSampleProjectGuid`: this is the project flavor GUID and should be your 
  own unique value too. Update the value in `ProjectTemplate.csproj` to match.
* `source.extension.vsixmanifest`: change the `Identity` element, in particular, the `Id` attribute.

