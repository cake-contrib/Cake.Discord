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
    /// Class that lets you override default API settings
    /// </summary>
    [CakeAliasCategory("Discord")]
    public sealed class DiscordChatMessageSettings
    {
        /// <summary>
        /// Name of bot.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// URL to an image to use as the icon for this message
        /// </summary>
        public Uri IconUrl { get; set; }

        /// <summary>
        /// Optional flag for if should throw exception on failure
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}
