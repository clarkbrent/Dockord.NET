using Dockord.Library.Events;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Dockord.Library.Tests.Events
{
    public class DockordEventIdTestData : TheoryData<EventId>
    {
        public DockordEventIdTestData()
        {
            var events = typeof(DockordEventId).GetProperties();

            foreach (var e in events)
            {
                if (e.GetValue(events) is EventId dockordEvent)
                    Add(dockordEvent);
            }
        }
    }
}
