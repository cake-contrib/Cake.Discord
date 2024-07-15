using System.Text;
using Cake.Core.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Cake.Discord.Chat
{
    /// <summary>
    /// The result of DiscordProvider Chat API post.
    /// </summary>
    [CakeAliasCategory("Discord")]
    public sealed class DiscordChatMessageResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordChatMessageResult"/> class.
        /// </summary>
        /// <param name="ok">Indicating success or failure.</param>
        /// <param name="timeStamp">Timestamp of the message.</param>
        /// <param name="errorCode">Error code on failure.</param>
        /// <param name="error">Error message on failure.</param>
        public DiscordChatMessageResult(bool ok, string timeStamp, int errorCode, string error)
        {
            Ok = ok;
            TimeStamp = timeStamp;
            ErrorCode = errorCode;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the result is a success or failure, <see cref="Error"/> for info on failure.
        /// </summary>
        public bool Ok { get; }

        /// <summary>
        /// Gets the timestamp of the message.
        /// </summary>
        public string TimeStamp { get; }

        /// <summary>
        /// Gets the error code on failure.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Gets the error message on failure.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Converts this instance of value to a string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{ Ok = ");
            builder.Append(Ok);
            builder.Append(", TimeStamp = ");
            builder.Append(TimeStamp);
            builder.Append(", ErrorCode = ");
            builder.Append(ErrorCode);
            builder.Append(", Error = ");
            builder.Append(Error);
            builder.Append(" }");
            return builder.ToString();
        }
    }
}
