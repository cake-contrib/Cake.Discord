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
        /// <returns>Returns success/error/timestamp <see cref="DiscordChatMessageResult"/></returns>
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
        /// <returns>Returns success/error/timestamp <see cref="DiscordChatMessageResult"/></returns>
        /// <param name="messageSettings">Lets you override default settings like UserName, or IconUrl.</param>
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
