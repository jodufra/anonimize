using System;
using System.Runtime.Serialization;

namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
    public abstract class AnonimizeNullException : ArgumentNullException
    {
        const string EXCEPTION_ARGUMENT_NULL = "'{0}' must not be null";

        protected AnonimizeNullException(string paramName) : base(paramName, string.Format(EXCEPTION_ARGUMENT_NULL, paramName))
        {
        }

        AnonimizeNullException()
        {
        }

        AnonimizeNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        AnonimizeNullException(string paramName, string message) : base(paramName, message)
        {
        }

        AnonimizeNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
