using Microsoft.Extensions.Logging;
using Xunit;

namespace Dockord.Library.Tests.Events
{
    public class DockordEventTests
    {
        [Theory]
        [ClassData(typeof(DockordEventIdTestData))]
        public void DockordEventIds_ShouldHaveIdGreaterThanZero(EventId dockordEvent)
        {
            Assert.True(dockordEvent.Id > 0);
        }

        [Theory]
        [ClassData(typeof(DockordEventIdTestData))]
        public void DockordEventIds_ShouldHaveNonNullStringName(EventId dockordEvent)
        {
            bool eventNameIsString = dockordEvent.Name.GetType() == typeof(string);

            Assert.True(eventNameIsString);
            Assert.False(string.IsNullOrWhiteSpace(dockordEvent.Name));
        }
    }
}
