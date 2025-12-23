#load nuget:https://pkgs.dev.azure.com/cake-contrib/Home/_packaging/addins/nuget/v3/index.json?package=Cake.Recipe&version=4.1.0-alpha0042

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Discord",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Discord",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunDotNetPack: true,
                            shouldRunCodecov: false,
                            preferredBuildProviderType: BuildProviderType.GitHubActions,
                            preferredBuildAgentOperatingSystem: PlatformFamily.Windows);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolPreprocessorDirectives(
  gitReleaseManagerGlobalTool: "#tool dotnet:?package=GitReleaseManager.Tool&version=0.18.0");

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNet();
