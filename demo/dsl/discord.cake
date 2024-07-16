#r "../../Source/Cake.Discord/bin/Debug/netstandard2.0/Cake.Discord.dll"
//#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Discord"

var url = Argument<string>("url", null);

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
        content:"This is a normal message."
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
        content:"This is a TTS message.",
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
        content:"This is a custom avatar and name message.",
        messageSettings:new DiscordChatMessageSettings {
            UserName = "gep13",
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
        content:"This _is_ a `message` with custom formatting from *CakeBuild* using incoming web hook:thumbsup:\r\n```Here is some code```"
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

Console.ReadLine();
