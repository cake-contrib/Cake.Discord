#load nuget:?package=Cake.Recipe&version=1.1.2

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Discord",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Discord",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/Source/Cake.Discord/**/*.AssemblyInfo.cs",
                                BuildParameters.RootDirectoryPath + "/Source/Cake.Discord/LitJson/**/*.cs" });

Build.RunDotNetCore();
