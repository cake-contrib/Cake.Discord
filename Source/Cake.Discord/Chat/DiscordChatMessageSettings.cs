using System;
using Cake.Core.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Cake.Discord.Chat
{
    /// <summary>
    /// Class that lets you override default API settings.
    /// </summary>
    [CakeAliasCategory("Discord")]
    public sealed class DiscordChatMessageSettings
    {
        /// <summary>
        /// Gets or sets the name of the bot.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the URL to an image to use as the avatar for this message.
        /// </summary>
        public Uri AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this message is a
        /// Text to Speech message.
        /// </summary>
        public bool Tts { get; set; }

        /// <summary>
        /// Gets or sets whether to throw an exception on failure.
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}
