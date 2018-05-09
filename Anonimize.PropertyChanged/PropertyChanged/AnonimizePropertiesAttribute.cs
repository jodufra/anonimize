using System;

namespace Anonimize.PropertyChanged
{
    /// <summary>
    /// Represents the attribute that registers the class type and properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AnonimizePropertiesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonimizePropertiesAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        public AnonimizePropertiesAttribute(Type type, params string[] properties)
        {
            AnonimizeProvider.GetInstance().GetPropertyChangedService().Register(type, properties);
        }
    }
}