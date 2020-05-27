# C# -> TypeScript Converter
## Usage
### Mark required classes with [TypeScriptViewModel] attribute
A class marked with the attribute will be picked up for TypeScript conversion in the configured build task.

```
[TypeScriptViewModel]
public class MyViewModel {
	public string ThisIsAString {get; set;}
	public int ThisIsAnInteger {get; set;}
}
```

### Set up the build task in the target .csproj project
The project containing the TypeScript classes for conversion will require a build action in the csproj file similar to the following:
```
  <!-- Begin TypeScript Build Step -->
  <Target Name="TypeScriptBuild" AfterTargets="Build">
    <PropertyGroup>
      <TypeScriptProjectName>My.Project.Name.Here</TypeScriptProjectName>
      <TypeScriptConverterVersion Condition="'%(PackageReference.Identity)' == 'Gman.TypeScript.Converter'">%(PackageReference.Version)</TypeScriptConverterVersion>
      <TypeScriptConverterDllPath>$(NuGetPackageRoot)gman.typescript.converter\$(TypeScriptConverterVersion)\lib\$(TargetFramework)\TypeScript.Modeller.dll</TypeScriptConverterDllPath>
      <TypeScriptSourceDllPath>$(SolutionDir)\$(TypeScriptProjectName)\bin\$(Configuration)\$(TargetFramework)\$(AssemblyName).dll</TypeScriptSourceDllPath>
      <TypeScriptOutputPath>$(SolutionDir)output\models\</TypeScriptOutputPath>
    </PropertyGroup>
    <Exec Command="dotnet $(TypeScriptConverterDllPath) $(TypeScriptSourceDllPath) $(TypeScriptOutputPath)" ContinueOnError="true">
      <Output TaskParameter="ExitCode" PropertyName="ErrorCode" />
    </Exec>
    <Error Condition="'$(ErrorCode)' != '0'" Text="The TypeScript conversion tool encountered an error." />
  </Target>
  <!-- End TypeScript Build Step -->
```

### Rebuild the TypeScript conversion project
When this rebuilds, it will now attempt the TypeScript conversion for the marked classes.

### Referenced assemblies throwing 'File Not Found' error
This step should not be required if all of your TypeScript classes are contained within the targeted build project. Sometimes, however, you may reference a class in an external DLL.
If this occurs, you will need to mark the target TypeScript project with the ```CopyLocalLockFileAssemblies``` attribute set to true; this will pull in local copies of 
the referenced assemblies (at build time) in order to access them for referenced conversion.
```
<PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
</PropertyGroup>
```