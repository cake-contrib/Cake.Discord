#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Discord",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Discord",
                            appVeyorAccountName: "cakecontrib");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/Source/Cake.Discord/**/*.AssemblyInfo.cs",
                                BuildParameters.RootDirectoryPath + "/Source/Cake.Discord/LitJson/**/*.cs" });

Build.RunDotNetCore();
