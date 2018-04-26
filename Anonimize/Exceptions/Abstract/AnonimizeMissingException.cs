using System;
using System.Runtime.Serialization;

namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist or that is not declared as public. If a member in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
    public abstract class AnonimizeMissingException : MissingMemberException
    {
        const string MESSAGE_MISSING = "Property named '{0}' of type '{1}' not found in type '{2}'";
        
        protected AnonimizeMissingException(string className, string memberName, string memberReturnTypeName) : 
            base(string.Format(MESSAGE_MISSING, memberName, memberReturnTypeName, className))
        {
        }

        AnonimizeMissingException()
        {
        }

        AnonimizeMissingException(string message) : base(message)
        {
        }

        AnonimizeMissingException(string message, Exception inner) : base(message, inner)
        {
        }

        AnonimizeMissingException(string className, string memberName) : base(className, memberName)
        {
        }

        AnonimizeMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
