using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Discord
{
    /// <summary>
    /// Contains aliases related to DiscordS API
    /// </summary>
    [CakeAliasCategory("Discord")]
    // ReSharper disable once UnusedMember.Global
    public static class DiscordAliases
    {
        /// <summary>
        /// Gets a <see cref="DiscordProvider"/> instance that can be used for communicating with Discord API.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="DiscordProvider"/> instance.</returns>
        [CakePropertyAlias(Cache = true)]
        [CakeNamespaceImportAttribute("Cake.Discord.Chat")]
        // ReSharper disable once UnusedMember.Global
        public static DiscordProvider Discord(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return new DiscordProvider(context);
        }
    }
}
