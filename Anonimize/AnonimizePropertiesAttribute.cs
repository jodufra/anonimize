using System;

namespace Anonimize
{
    /// <summary>
    /// Represents the attribute that registers the class type and the properties in <see cref="Anonimize.AnonimizeService"/>.
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
            Anonimize.AnonimizeService.Register(type, properties);
        }
    }
}