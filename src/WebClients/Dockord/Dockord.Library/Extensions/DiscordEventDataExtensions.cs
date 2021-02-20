using Dockord.Library.Models;
using System.Text;

namespace Dockord.Library.Extensions
{
    public static class DiscordEventDataExtensions
    {
        /// <summary>
        /// Creates a tuple containing a log message <see cref="string"/> template created from the
        /// property names of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>, <br />
        /// and an <see cref="object"/>[] containing the property values of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>.
        /// </summary>
        /// <remarks>
        /// Filters out null valued properties. <br />
        /// Guarantees matching order between the log template <see cref="string"/> parameters, and their corresponding property value <see cref="object"/>[] <see cref="args"/>.
        /// </remarks>
        /// <returns>
        /// A tuple containing:<br />
        /// A log message template <see cref="string"/> created from the property names of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>.<br />
        /// An <see cref="object"/>[] containing the property values of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>.
        /// </returns>
        public static (string, object[]) ToEventLogTuple(this DiscordEventDataModel discordEvent, string message = "")
        {
            int counter = 0;
            var properties = discordEvent.GetType().GetProperties();
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(message))
                sb.Append(message + " ");

            // Get total count of all non-null properties
            foreach (var property in properties)
            {
                if (property.GetValue(discordEvent) != null)
                    counter += 1;
            }

            var args = new object[counter];
            counter = 0;
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(discordEvent);
                if (propertyValue == null)
                    continue;

                sb.Append($"{{{property.Name}}} ");
                args[counter] = propertyValue;

                counter += 1;
            }

            sb.Remove(sb.Length - 1, 1); // Remove trailing space

            return (sb.ToString(), args);
        }
    }
}
