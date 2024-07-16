#r "../../Source/Cake.Discord/bin/Debug/net8.0/Cake.Discord.dll"
//#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Discord"

var url = Argument<string>("url", null);

var cakeAssembly = typeof(ICakeContext).Assembly.GetName();
var cakeName = $"{cakeAssembly.Name ?? "UNKNOWN"} v{cakeAssembly.Version?.ToString() ?? "??.??.??"}";

if (string.IsNullOrEmpty(url))
{
    Error("you need to pass a webhook url via `--url=...`");
    return;
}

try
{
    Information("This is a 'normal' message...");

    var postMessageResult = Discord.Chat.PostMessage(
        webHookUrl:url,
        content:$"This is a normal message from {cakeName}."
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} successfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}

try
{
    Information("This is a 'tts' message...");

    var postMessageResult = Discord.Chat.PostMessage(
        webHookUrl:url,
        content:$"This is a TTS message from {cakeName}.",
        messageSettings:new DiscordChatMessageSettings { Tts = true }
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} successfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}

try
{
    Information("This is a custom avatar and name message...");

    var postMessageResult = Discord.Chat.PostMessage(
        webHookUrl:url,
        content:$"This is a custom avatar and name message from {cakeName}.",
        messageSettings:new DiscordChatMessageSettings {
            UserName = cakeName,
            AvatarUrl = new Uri("https://avatars0.githubusercontent.com/u/1271146?s=400&v=4")
            }
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} successfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}

try
{
    Information("This is a message with custom formatting...");

    var postMessageResult = Discord.Chat.PostMessage(
        webHookUrl:url,
        content:$"This _is_ a `message` with custom formatting from *CakeBuild* using incoming web hook:thumbsup:\r\n```Here is some code``` from {cakeName}"
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} successfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}

Information("Any key to continue.");
Console.ReadLine();
