using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize
{
    /// <summary>Represents the base class for custom attributes.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AnonimizePropertiesAttribute : Attribute
    {
        readonly Type classType;

        readonly HashSet<string> decryptedProperties;

        readonly HashSet<string> encryptedProperties;

        public AnonimizePropertiesAttribute(Type type, params string[] properties)
        {
            encryptedProperties = new HashSet<string>();
            decryptedProperties = new HashSet<string>();
            classType = type ?? throw new ArgumentNullException(nameof(type), "Type must not be null");

            foreach (var property in properties)
            {
                AddDecryptable(property);
                AddEncryptable(property);
            }
        }

        public Type ClassType
        {
            get
            {
                return classType;
            }
        }

        public HashSet<string> DecryptedProperties
        {
            get
            {
                return decryptedProperties;
            }
        }

        public HashSet<string> EncryptedProperties
        {
            get
            {
                return encryptedProperties;
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

        /// <summary>
        /// Asserts if the type has a property with the specified name that returns a string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException">If property is missing or it doesn't return type of string</exception>
        static void AssertTypeHasProperty(Type type, string name)
        {
            var typeofString = typeof(string);
            try
            {
                var prop = type.GetProperty(name, typeofString);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Property '{name}' is missing in type '{type.Name}' or it doesn't return '{typeofString.Name}'", nameof(name));
            }
        }

        void AddDecryptable(string property)
        {
            property = AnonimizeService.ToDecryptablePropertyName(property);

            if (IsDecryptable(property))
                return;

            AssertTypeHasProperty(classType, property);

            decryptedProperties.Add(property);
        }

        void AddEncryptable(string property)
        {
            property = AnonimizeService.ToEncryptablePropertyName(property);

            if (IsEncryptable(property))
                return;

            AssertTypeHasProperty(classType, property);

            encryptedProperties.Add(property);
        }
    }
}