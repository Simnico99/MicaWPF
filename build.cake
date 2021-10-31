var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./";

Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => 
    {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
			Framework = "net6.0-windows",
            NoRestore = true,
            Configuration = configuration
        });
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
			Framework = "net5.0-windows",
            NoRestore = true,
            Configuration = configuration
        });
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
			Framework = "netcoreapp3.1",
            NoRestore = true,
            Configuration = configuration
        });
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
			Framework = "net48",
            NoRestore = true,
            Configuration = configuration
        });
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
			Framework = "net472",
            NoRestore = true,
            Configuration = configuration
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true
        });
    });

RunTarget(target);