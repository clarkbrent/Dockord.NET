using System;

namespace Dockord.Library.Exceptions
{
    public class InteractivityTimedOutException : Exception
    {
        public InteractivityTimedOutException() : base()
        {
        }

        public InteractivityTimedOutException(string? message) : base(message)
        {
        }

        public InteractivityTimedOutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
