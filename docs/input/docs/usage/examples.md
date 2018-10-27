---
Title: Examples
---

# Post Message

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
