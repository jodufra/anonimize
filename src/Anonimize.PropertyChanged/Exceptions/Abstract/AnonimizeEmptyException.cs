using System;
using System.Runtime.Serialization;

namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
    public abstract class AnonimizeEmptyException : ArgumentNullException
    {
        const string MESSAGE_EMPTY = "'{0}' must not be empty";

        protected AnonimizeEmptyException(string paramName) : base(paramName, string.Format(MESSAGE_EMPTY, paramName))
        {
        }

        AnonimizeEmptyException()
        {
        }

        AnonimizeEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        AnonimizeEmptyException(string paramName, string message) : base(paramName, message)
        {
        }

        AnonimizeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
