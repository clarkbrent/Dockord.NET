using Dockord.Library.Extensions;
using Dockord.Library.Models;
using Xunit;

namespace Dockord.Library.Tests.Extensions
{
    public class DiscordEventExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ValidDiscordEventModels))]
        public void ToEventLogTuple_ValidDiscordEventShouldReturnValidEventLogTuple(IDiscordEvent discordEvent)
        {
            (string logMessageTemplate, object[] logArgs) = discordEvent.ToEventLogTuple();
            bool ArgsLengthIsValidIDiscordEventLength = logArgs.Length <= discordEvent.GetType().GetProperties().Length;

            if (discordEvent is CommandEventModel cem)
            {
                Assert.False(string.IsNullOrWhiteSpace(cem.CommandName));
                Assert.False(string.IsNullOrWhiteSpace(cem.Username));
            }

            Assert.False(string.IsNullOrWhiteSpace(logMessageTemplate));
            Assert.True(logArgs.Length > 0);
            Assert.True(ArgsLengthIsValidIDiscordEventLength);
        }

        [Theory]
        [ClassData(typeof(ValidDiscordEventModels))]
        public void ToEventLogTuple_WithValidMessage_ShouldReturnValidEventLogTuple(IDiscordEvent discordEvent)
        {
            const string expectedMessage = "Valid event message";
            (string logMessageTemplate, object[] logArgs) = discordEvent.ToEventLogTuple(expectedMessage);
            bool ArgsLengthIsValidIDiscordEventLength = logArgs.Length <= discordEvent.GetType().GetProperties().Length;

            if (discordEvent is CommandEventModel cem)
            {
                Assert.False(string.IsNullOrWhiteSpace(cem.CommandName));
                Assert.False(string.IsNullOrWhiteSpace(cem.Username));
            }

            Assert.False(string.IsNullOrWhiteSpace(logMessageTemplate));
            Assert.Contains(expectedMessage, logMessageTemplate);
            Assert.True(logArgs.Length > 0);
            Assert.True(ArgsLengthIsValidIDiscordEventLength);
        }
    }
}
