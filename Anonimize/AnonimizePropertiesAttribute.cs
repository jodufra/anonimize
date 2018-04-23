using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize
{
    /// <summary>Represents the base class for custom attributes.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AnonimizePropertiesAttribute : Attribute
    {
        readonly HashSet<string> encryptedProperties;
        readonly HashSet<string> decryptedProperties;
        readonly Type classType;

        public AnonimizePropertiesAttribute(Type type, params string[] properties)
        {
            encryptedProperties = new HashSet<string>();
            decryptedProperties = new HashSet<string>();
            classType = type;

            foreach (var property in properties)
            {
                AddDecryptable(ToDecryptablePropertyName(property));
                AddEncryptable(ToEncryptablePropertyName(property));
            }
        }

        public bool IsDecryptable(string property)
        {
            return decryptedProperties.Contains(property);
        }

        public bool IsEncryptable(string property)
        {
            return encryptedProperties.Contains(property);
        }

        public string ToDecryptablePropertyName(string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                throw new ArgumentOutOfRangeException(nameof(property), $"Invalid property name: '{property}'");

            if (!property.StartsWith("_"))
                return property;

            return property.TrimStart('_');
        }

        public string ToEncryptablePropertyName(string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                throw new ArgumentOutOfRangeException(nameof(property), $"Invalid property name: '{property}'");

            if (property.StartsWith("_"))
                return property;

            return $"_{property}";
        }

        void AddDecryptable(string property)
        {
            if (IsDecryptable(property))
                return;

            AssertHasProperty(property);

            decryptedProperties.Add(property);
        }

        void AddEncryptable(string property)
        {
            if (IsEncryptable(property))
                return;

            AssertHasProperty(property);

            encryptedProperties.Add(property);
        }

        void AssertHasProperty(string property)
        {
            try
            {
                var prop = classType.GetProperty(property, typeof(string));
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(property), $"Property '{property}' in type '{classType.Name}'");
            }
        }
    }
}