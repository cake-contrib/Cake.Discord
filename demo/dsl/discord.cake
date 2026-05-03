#r "../../Source/Cake.Discord/bin/Release/net10.0/Cake.Discord.dll"
//#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Discord"

var url = Argument<string>("url", EnvironmentVariable("DISCORD_WEBHOOK_URL", string.Empty));

var cakeAssembly = typeof(ICakeContext).Assembly.GetName();
var cakeName = $"{cakeAssembly.Name ?? "UNKNOWN"} v{cakeAssembly.Version?.ToString() ?? "??.??.??"}";

if (string.IsNullOrEmpty(url))
{
    Error("you need to pass a webhook url via `--url=...` or set the DISCORD_WEBHOOK_URL environment variable");
    return;
}

var sendResults = new List<(string Label, bool Ok, string Detail)>();

void Send(string label, string content, DiscordChatMessageSettings settings = null)
{
    Information("Sending: {0}...", label);
    try
    {
        var result = settings == null
            ? Discord.Chat.PostMessage(webHookUrl: url, content: content)
            : Discord.Chat.PostMessage(webHookUrl: url, content: content, messageSettings: settings);

        if (result.Ok)
        {
            sendResults.Add((label, true, result.TimeStamp));
            Information("  -> OK at {0}", result.TimeStamp);
        }
        else
        {
            sendResults.Add((label, false, result.Error ?? "(no error message)"));
            Error("  -> FAILED: {0}", result.Error);
        }
    }
    catch (Exception ex)
    {
        sendResults.Add((label, false, $"{ex.GetType().Name}: {ex.Message}"));
        Error("  -> EXCEPTION: {0}", ex);
    }
}

Send(
    "normal message",
    $"This is a normal message from {cakeName}.");

Send(
    "TTS message",
    $"This is a TTS message from {cakeName}.",
    new DiscordChatMessageSettings { Tts = true });

Send(
    "custom avatar and name",
    $"This is a custom avatar and name message from {cakeName}.",
    new DiscordChatMessageSettings
    {
        UserName = cakeName,
        AvatarUrl = new Uri("https://avatars0.githubusercontent.com/u/1271146?s=400&v=4"),
    });

Send(
    "custom formatting",
    $"This _is_ a `message` with custom formatting from *CakeBuild* using incoming web hook:thumbsup:\r\n```Here is some code``` from {cakeName}");

Information("");
Information("=== Summary ===");
var ok = sendResults.Count(r => r.Ok);
var total = sendResults.Count;
foreach (var r in sendResults)
{
    Information("  [{0}] {1} — {2}", r.Ok ? "OK" : "FAIL", r.Label, r.Detail);
}

Information("");
Information("{0} of {1} messages sent successfully.", ok, total);

if (ok != total)
{
    throw new Exception($"{total - ok} of {total} Discord webhook posts failed. See log above.");
}
