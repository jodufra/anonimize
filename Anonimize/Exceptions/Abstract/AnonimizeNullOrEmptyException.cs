using System;
using System.Runtime.Serialization;

namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
    public abstract class AnonimizeNullOrEmptyException : ArgumentNullException
    {
        const string MESSAGE_NULL_EMPTY_WHITESPACE = "'{0}' must not be null, or empty or only white spaces.";

        protected AnonimizeNullOrEmptyException(string paramName) : base(paramName, string.Format(MESSAGE_NULL_EMPTY_WHITESPACE, paramName))
        {
        }

        AnonimizeNullOrEmptyException()
        {
        }

        AnonimizeNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        AnonimizeNullOrEmptyException(string paramName, string message) : base(paramName, message)
        {
        }

        AnonimizeNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
