using Anonimize.Exceptions;
using Anonimize.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anonimize.PropertyChanged
{
    /// <summary>
    ///
    /// </summary>
    public class AnonimizeProperties
    {
        readonly AnonimizeService anonimize;

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
            anonimize = AnonimizeProvider.GetInstance();
            classType = type ?? throw new TypeNullException(nameof(type));

            if (properties == null)
                throw new PropertyNullException(nameof(properties));

            decryptedProperties = new HashSet<string>();
            encryptedProperties = new HashSet<string>();

            anonimize.AddPropertyDenominatorChangedHandler(OnPropertyDenominatorChanged);

            foreach (var property in properties)
            {
                AddProperty(property);
            }
        }

        ~AnonimizeProperties()
        {
            anonimize.RemovePropertyDenominatorChangedHandler(OnPropertyDenominatorChanged);
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
        /// <exception cref="PropertyMissingException">If property is missing or doesn't return type of string</exception>
        public void AddProperty(string propertyName)
        {
            AddDecryptedProperty(propertyName);
            AddEncryptedProperty(propertyName);
        }

        /// <summary>
        /// Determines whether the specified property is decrypted.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property is decrypted; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDecrypted(string property)
        {
            return decryptedProperties.Contains(property);
        }

        /// <summary>
        /// Determines whether the specified property is encrypted
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if the specified property is encrypted; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEncrypted(string property)
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
            propertyName = anonimize.ToDecryptedPropertyName(propertyName);

            if (IsDecrypted(propertyName))
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
            propertyName = anonimize.ToEncryptedPropertyName(propertyName);

            if (IsEncrypted(propertyName))
                return;

            AssertTypeHasStringProperty(classType, propertyName);

            encryptedProperties.Add(propertyName);
        }

        /// <summary>
        /// Called when [property denominator changed].
        /// </summary>
        void OnPropertyDenominatorChanged()
        {
            var properties = new List<string>();

            if (decryptedProperties.Any(p => p.StartsWith(anonimize.GetEncryptedPropertyDenominator(), StringComparison.Ordinal)) ||
                decryptedProperties.Any(p => p.StartsWith(anonimize.GetDecryptedPropertyDenominator(), StringComparison.Ordinal)))
            {
                properties.AddRange(decryptedProperties);
            }
            else if (encryptedProperties.Any(p => p.StartsWith(anonimize.GetDecryptedPropertyDenominator(), StringComparison.Ordinal)) ||
                encryptedProperties.Any(p => p.StartsWith(anonimize.GetEncryptedPropertyDenominator(), StringComparison.Ordinal)))
            {
                properties.AddRange(encryptedProperties);
            }

            if (!properties.Any())
                return;

            decryptedProperties.Clear();
            encryptedProperties.Clear();

            foreach (var property in properties)
            {
                AddProperty(property);
            }
        }
    }
}