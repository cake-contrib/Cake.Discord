#r "bin/Release/netstandard2.0/Cake.Discord.dll"

try
{
    var postMessageResult = Discord.Chat.PostMessage(
        webHookUrl:"https://discordapp.com/api/webhooks/491746448291921920/vhj3a4thXNZldKXdcoJQfDrfkD-hHZVYbGC6hnR58c0yB1erFDfJ8KWU5dyFD-t12RKP",
        content:"This _is_ a `message` from *CakeBuild* using incoming web hook:thumbsup:\r\n```Here is some code```"
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
