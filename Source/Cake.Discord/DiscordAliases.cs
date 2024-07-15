using System;
using Cake.Core;
using Cake.Core.Annotations;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
namespace Cake.Discord
{
    /// <summary>
    /// Contains aliases related to DiscordS API.
    /// </summary>
    [CakeAliasCategory("Discord")]
    public static class DiscordAliases
    {
        /// <summary>
        /// Gets a <see cref="DiscordProvider"/> instance that can be used for communicating with Discord API.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="DiscordProvider"/> instance.</returns>
        [CakePropertyAlias(Cache = true)]
        [CakeNamespaceImport("Cake.Discord.Chat")]

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
