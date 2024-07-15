#load nuget:?package=Cake.Recipe&version=3.1.1

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Discord",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Discord",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunDotNetCorePack: true,
                            shouldRunCodecov: false,
                            shouldPostToGitter: false,
                            preferredBuildProviderType: BuildProviderType.GitHubActions,
                            preferredBuildAgentOperatingSystem: PlatformFamily.Windows);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolPreprocessorDirectives(
  gitReleaseManagerGlobalTool: "#tool dotnet:?package=GitReleaseManager.Tool&version=0.18.0");

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();
