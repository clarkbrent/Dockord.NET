using Dockord.Library.Exceptions;
using System;
using Xunit;

namespace Dockord.Library.Tests.Exceptions
{
    public class InteractivityTimedOutExceptionTests
    {
        private const string _expectedMessage = "Expected test message";

        [Fact]
        public async void InteractivityTimedOutException_ShouldWorkWithoutAnyArguments()
        {
            Exception ex = await Assert.ThrowsAsync<InteractivityTimedOutException>(() =>
                throw new InteractivityTimedOutException()).ConfigureAwait(false);

            Assert.Equal("Interactivity event timed out.", ex.Message);
        }

        [Fact]
        public async void InteractivityTimedOutException_ShouldWorkWithOnlyMessage()
        {
            Exception ex = await Assert.ThrowsAsync<InteractivityTimedOutException>(() =>
                throw new InteractivityTimedOutException(_expectedMessage)).ConfigureAwait(false);

            Assert.Equal(_expectedMessage, ex.Message);
        }

        [Fact]
        public async void InteractivityTimedOutException_ShouldWorkWithMessageAndInnerException()
        {
            Exception expectedInnerException = new Exception("First, and expected inner exception message");
            Exception unexpectedInnerException = new Exception("Second, but unexpected inner exception");

            Exception ex = await Assert.ThrowsAsync<InteractivityTimedOutException>(() =>
                    throw new InteractivityTimedOutException(_expectedMessage, expectedInnerException)).ConfigureAwait(false);

            Assert.Equal(_expectedMessage, ex.Message);
            Assert.Equal(expectedInnerException, ex.InnerException);
            Assert.Equal(expectedInnerException.Message, ex.InnerException!.Message);
            Assert.NotEqual(unexpectedInnerException, ex.InnerException);
            Assert.NotEqual(unexpectedInnerException.Message, ex.InnerException!.Message);
        }

        [Fact]
        public async void InteractivityTimedOutException_ShouldThrowArgumentExceptionWithNullMessage()
        {
            Exception expectedInnerException = new Exception("First, and expected inner exception message");

            await Assert.ThrowsAsync<ArgumentException>(() =>
                    throw new InteractivityTimedOutException(null!)).ConfigureAwait(false);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                    throw new InteractivityTimedOutException(null!, expectedInnerException)).ConfigureAwait(false);
        }
    }
}
