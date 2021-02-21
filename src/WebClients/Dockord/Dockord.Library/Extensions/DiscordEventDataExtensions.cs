using Dockord.Library.Models;
using System.Text;

namespace Dockord.Library.Extensions
{
    public static class DiscordEventDataExtensions
    {
        /// <summary>
        /// Creates a tuple that contains a log message <see cref="string"/> template, and an <see cref="object"/>[] containing the log message template's arguments.
        /// </summary>
        /// <remarks>
        /// Guarantees matching order between the log template <see cref="string"/> parameters, and their corresponding property value <see cref="object"/>[] <see cref="args"/>.
        /// </remarks>
        /// <returns>
        /// A tuple containing:<br />
        /// - A log message template <see cref="string"/> created from the property names of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>.<br />
        /// - An <see cref="object"/>[] containing the property values of <see cref="DiscordEventDataModel"/> <paramref name="discordEvent"/>.
        /// </returns>
        public static (string, object[]) ToEventLogTuple(this IDiscordEventDataModel discordEvent, string message = "")
        {
            int counter = 0;
            var properties = discordEvent.GetType().GetProperties();
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(message))
                sb.Append(message).Append(' ');

            // Get total count of all non-null properties from the event
            foreach (var property in properties)
            {
                if (property.GetValue(discordEvent) != null)
                    counter++;
            }

            var args = new object[counter];
            counter = 0;
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(discordEvent);
                if (propertyValue == null)
                    continue;

                sb.Append('{').Append(property.Name).Append("} ");
                args[counter] = propertyValue;

                counter++;
            }

            sb.Remove(sb.Length - 1, 1); // Remove trailing space

            return (sb.ToString(), args);
        }
    }
}
