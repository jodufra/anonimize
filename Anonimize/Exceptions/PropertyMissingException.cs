namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist or that is not declared as public. If a member in a class library has been removed or renamed, recompile any assemblies that reference that library.</summary>
    public class PropertyMissingException : AnonimizeMissingException
    {
        public PropertyMissingException(string className, string memberName, string memberReturnTypeName) : base(className, memberName, memberReturnTypeName)
        {
        }
    }
}
