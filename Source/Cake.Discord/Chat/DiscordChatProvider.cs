using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

// ReSharper disable UnusedMember.Global
namespace Cake.Discord.Chat
{
    /// <summary>
    /// Contains <see href="https://discordapp.com/developers/docs/resources/webhook">Discord API</see> Chat functionality.
    /// </summary>
    [CakeAliasCategory("Discord")]
    public sealed class DiscordChatProvider
    {
        private readonly ICakeContext _context;

        /// <summary>
        /// Post message to Discord WebHook
        /// </summary>
       /// <param name="webHookUrl">The URL for the webhook that messages should be sent to.</param>
        /// <param name="content">Content of the message to send.</param>
        /// <returns>Returns success/errorcode/error/timestamp <see cref="DiscordChatMessageResult"/></returns>
        /// <example>
        /// <code>
        ///     Information("This is a 'normal' message...");
        ///
        ///     var postMessageResult = Discord.Chat.PostMessage(
        ///        webHookUrl:"https://your_web_hook_url",
        ///        content:"This is a normal message."
        ///     );
        ///
        ///     if (postMessageResult.Ok)
        ///     {
        ///         Information("Message {0} successfully sent", postMessageResult.TimeStamp);
        ///     }
        ///     else
        ///     {
        ///         Error("Failed to send message: {0}", postMessageResult.Error);
        ///     }
        /// </code>
        /// </example>
        [CakeAliasCategory("Chat")]
        public DiscordChatMessageResult PostMessage(
            string webHookUrl,
            string content
            )
        {
            var response = _context.PostMessage(
                webHookUrl,
                content,
                new DiscordChatMessageSettings()
                );
            response.Wait();

            return response.Result;
        }

        /// <summary>
        /// Post message to Discord WebHook
        /// </summary>
        /// <param name="webHookUrl">The URL for the webhook that messages should be sent to.</param>
        /// <param name="content">Content of the message to send.</param>
        /// <param name="messageSettings">Lets you override default settings like UserName, or IconUrl.</param>
        /// <returns>Returns success/error/timestamp <see cref="DiscordChatMessageResult"/></returns>
        /// <example>
        /// <code>
        ///    Information("This is a custom avatar and name message...");
        ///
        ///    var postMessageResult = Discord.Chat.PostMessage(
        ///        webHookUrl:"https://your_web_hook_url",
        ///        content:"This is a custom avatar and name message.",
        ///        messageSettings:new DiscordChatMessageSettings {
        ///            UserName = "gep13",
        ///            AvatarUrl = new Uri("https://avatars0.githubusercontent.com/u/1271146?s=400%26v=4")
        ///            }
        ///        );
        ///
        ///    if (postMessageResult.Ok)
        ///    {
        ///        Information("Message {0} successfully sent", postMessageResult.TimeStamp);
        ///    }
        ///    else
        ///    {
        ///        Error("Failed to send message: {0}", postMessageResult.Error);
        ///    }
        /// </code>
        /// </example>
        [CakeAliasCategory("Chat")]
        public DiscordChatMessageResult PostMessage(
            string webHookUrl,
            string content,
            DiscordChatMessageSettings messageSettings
            )
        {
            if (messageSettings == null)
            {
                throw new ArgumentNullException(nameof(messageSettings));
            }

            var response = _context.PostMessage(
                webHookUrl,
                content,
                messageSettings);

            response.Wait();

            return response.Result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordChatProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DiscordChatProvider(ICakeContext context)
        {
            _context = context;
        }
    }
}
