using Cake.Core;
using Cake.Discord.Chat;

namespace Cake.Discord
{
    /// <summary>
    /// Contains functionality related to Discord API.
    /// </summary>
    public sealed class DiscordProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DiscordProvider(ICakeContext context)
        {
            Chat = new DiscordChatProvider(context);
        }

        /// <summary>
        /// Gets the Discord Chat functionality.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public DiscordChatProvider Chat { get; }
    }
}
