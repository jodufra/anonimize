using Anonimize.Exceptions;
using Anonimize.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anonimize
{
    /// <summary>
    /// 
    /// </summary>
    public class AnonimizeProperties
    {
        readonly Type classType;

        readonly HashSet<string> decryptedProperties;

        readonly HashSet<string> encryptedProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonimizeProperties"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        /// <exception cref="TypeNullException">type is null</exception>
        /// <exception cref="PropertyNullException">properties is null</exception>
        public AnonimizeProperties(Type type, params string[] properties)
        {
            classType = type ?? throw new TypeNullException(nameof(type));

            if (properties == null)
                throw new PropertyNullException(nameof(properties));

            decryptedProperties = new HashSet<string>();
            encryptedProperties = new HashSet<string>();

            foreach (var property in properties)
            {
                AddProperty(property);
            }
        }

        /// <summary>
        /// Gets the type of the class.
        /// </summary>
        /// <value>
        /// The type of the class.
        /// </value>
        public Type ClassType
        {
            get => classType;
        }

        /// <summary>
        /// Gets the decrypted properties.
        /// </summary>
        /// <value>
        /// The decrypted properties.
        /// </value>
        public HashSet<string> DecryptedProperties
        {
            get => decryptedProperties;
        }

        /// <summary>
        /// Gets the encrypted properties.
        /// </summary>
        /// <value>
        /// The encrypted properties.
        /// </value>
        public HashSet<string> EncryptedProperties
        {
            get => encryptedProperties;
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="PropertyMissingException">If property is missing or it doesn't return type of string</exception>
        public void AddProperty(string propertyName)
        {
            AddDecryptedProperty(propertyName);
            AddEncryptedProperty(propertyName);
        }

        /// <summary>
        /// Determines whether the specified property is decryptable.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property is decryptable; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDecryptable(string property)
        {
            return decryptedProperties.Contains(property);
        }

        /// <summary>
        /// Determines whether the specified property is encryptable.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property is encryptable; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEncryptable(string property)
        {
            return encryptedProperties.Contains(property);
        }

        /// <summary>
        /// Asserts the type has a string property with the specified name
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="PropertyMissingException">If property is missing or it doesn't return type of string</exception>
        static void AssertTypeHasStringProperty(Type type, string name)
        {
            var returnType = typeof(string);
            var property = type.GetProperty(name, returnType);

            if (property == null)
                throw new PropertyMissingException(type.Name, name, returnType.Name);
        }

        /// <summary>
        /// Adds the decrypted property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        void AddDecryptedProperty(string propertyName)
        {
            propertyName = Anonimize.ToDecryptedPropertyName(propertyName);

            if (IsDecryptable(propertyName))
                return;

            AssertTypeHasStringProperty(classType, propertyName);

            decryptedProperties.Add(propertyName);
        }

        /// <summary>
        /// Adds the encrypted property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        void AddEncryptedProperty(string propertyName)
        {
            propertyName = Anonimize.ToEncryptedPropertyName(propertyName);

            if (IsEncryptable(propertyName))
                return;

            AssertTypeHasStringProperty(classType, propertyName);

            encryptedProperties.Add(propertyName);
        }
    }
}