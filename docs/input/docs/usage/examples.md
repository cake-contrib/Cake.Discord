---
Title: Examples
---

# Post Message

In order to use Cake.Discord, you will need to add the following to your Cake script:

```csharp
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Discord&version=0.1.0"
```

**NOTE:** Depending on the currently released version, you might want to change the above to reflect the current version number.  The above is shown to ensure that the best practice of pinning your Cake Addin version numbers is adhered to.

## Using Webhook URL

### Posting a message without any settings

```csharp
Information("This is a 'normal' message...");

var postMessageResult = Discord.Chat.PostMessage(
    webHookUrl:"https://your_web_hook_url",
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
```

### Posting a message using DiscordChatMessageSettings

```csharp
Information("This is a custom avatar and name message...");

var postMessageResult = Discord.Chat.PostMessage(
    webHookUrl:"https://your_web_hook_url",
    content:"This is a custom avatar and name message.",
    messageSettings: new DiscordChatMessageSettings {
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
```
