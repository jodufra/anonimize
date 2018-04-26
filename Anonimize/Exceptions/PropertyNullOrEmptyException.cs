namespace Anonimize.Exceptions
{
    /// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
    public class PropertyNullOrEmptyException : AnonimizeNullOrEmptyException
    {
        public PropertyNullOrEmptyException(string paramName) : base(paramName)
        {
        }
    }
}
