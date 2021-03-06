using System;

namespace Dockord.Library.Exceptions
{
    public class InteractivityTimedOutException : Exception
    {
        public InteractivityTimedOutException() : base("Interactivity event timed out.")
        {
        }

        public InteractivityTimedOutException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty", message);
        }

        public InteractivityTimedOutException(string message, Exception innerException) : base(message, innerException)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty", message);
        }
    }
}
