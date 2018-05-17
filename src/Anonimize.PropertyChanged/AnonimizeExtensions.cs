using Anonimize.Exceptions;
using Anonimize.Services;
using System;

namespace Anonimize
{
    public static class AnonimizeExtensions
    {
        static readonly Lazy<IPropertyChangedService> propertyChangedServiceDefault = new Lazy<IPropertyChangedService>(() => new PropertyChangedService());

        static IPropertyChangedService propertyChangedServiceOverride;

        static string encryptedPropertyDenominator = "_";

        static readonly string encryptedPropertyDenominatorDefault = "_";

        static string decryptedPropertyDenominator = string.Empty;

        static readonly string decryptedPropertyDenominatorDefault = string.Empty;

        static event PropertyDenominatorChangedEventHandler PropertyDenominatorChanged;

        /// <summary>
        /// Represents the method that will handle the <see cref="PropertyDenominatorChanged"/> event raised when a property denominator is changed.
        /// </summary>
        public delegate void PropertyDenominatorChangedEventHandler();

        /// <summary>
        /// Adds the property denominator changed handler.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="eventHandler">The event handler.</param>
        public static void AddPropertyDenominatorChangedHandler(this AnonimizeService anonimize, PropertyDenominatorChangedEventHandler eventHandler)
        {
            PropertyDenominatorChanged += eventHandler;
        }

        /// <summary>
        /// Removes the property denominator changed handler.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="eventHandler">The event handler.</param>
        public static void RemovePropertyDenominatorChangedHandler(this AnonimizeService anonimize, PropertyDenominatorChangedEventHandler eventHandler)
        {
            PropertyDenominatorChanged -= eventHandler;
        }

        /// <summary>
        /// Gets the default lazily initialized instance of <see cref="PropertyChangedService" />.
        /// If set to other <see cref="IPropertyChangedService" />, then returns the instance of that service.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <returns>
        /// The anonimize service.
        /// </returns>
        public static IPropertyChangedService GetPropertyChangedService(this AnonimizeService anonimize)
        {
            return propertyChangedServiceOverride ?? propertyChangedServiceDefault.Value;
        }

        /// <summary>
        /// Sets the anonimize service.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="service">The service.</param>
        public static void SetPropertyChangedService(this AnonimizeService anonimize, IPropertyChangedService service)
        {
            propertyChangedServiceOverride = service;
        }

        /// <summary>
        /// Gets the starting denominator of the encrypted properties. Default <c>_</c>.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <example>
        /// If value is _ then the related encrypted property of <c>Name</c> is <c>_Name</c>
        /// </example>
        /// <returns>
        /// The encrypted property denominator.
        /// </returns>
        public static string GetEncryptedPropertyDenominator(this AnonimizeService anonimize)
        {
            return encryptedPropertyDenominator;
        }

        /// <summary>
        /// Sets the encrypted property denominator.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="value">The value.</param>
        public static void SetEncryptedPropertyDenominator(this AnonimizeService anonimize, string value)
        {
            anonimize.SetPropertyDenominators(value, decryptedPropertyDenominator);
        }

        /// <summary>
        /// Gets or sets the starting denominator of the decrypted properties. Default <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <example>
        /// If value is <see cref="string.Empty"/> then the related decrypted property of <c>_Name</c> is <c>Name</c>
        /// </example>
        /// <returns>
        /// The decrypted property denominator.
        /// </returns>
        public static string GetDecryptedPropertyDenominator(this AnonimizeService anonimize)
        {
            return decryptedPropertyDenominator;
        }

        /// <summary>
        /// Sets the decrypted property denominator.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="value">The value.</param>
        public static void SetDecryptedPropertyDenominator(this AnonimizeService anonimize, string value)
        {
            anonimize.SetPropertyDenominators(encryptedPropertyDenominator, value);
        }

        /// <summary>
        /// Sets the default property denominators.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        public static void SetDefaultPropertyDenominators(this AnonimizeService anonimize)
        {
            anonimize.SetPropertyDenominators(encryptedPropertyDenominatorDefault, decryptedPropertyDenominatorDefault);
        }

        /// <summary>
        /// Sets the property denominators.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="encryptedPropDenominator">The encrypted property denominator.</param>
        /// <param name="decryptedPropDenominator">The decrypted property denominator.</param>
        /// <exception cref="ArgumentNullException">
        /// encryptedPropertyDenominator is null
        /// or
        /// decryptedPropertyDenominator is null
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">encryptedPropertyDenominator equals decryptedPropertyDenominator</exception>
        public static void SetPropertyDenominators(this AnonimizeService anonimize, string encryptedPropDenominator, string decryptedPropDenominator)
        {
            if (encryptedPropDenominator == null)
                throw new ArgumentNullException(nameof(encryptedPropDenominator));

            if (decryptedPropDenominator == null)
                throw new ArgumentNullException(nameof(decryptedPropDenominator));

            if (encryptedPropDenominator == decryptedPropDenominator)
                throw new ArgumentOutOfRangeException();

            encryptedPropertyDenominator = encryptedPropDenominator;
            decryptedPropertyDenominator = decryptedPropDenominator;

            PropertyDenominatorChanged?.Invoke();
        }

        /// <summary>
        /// To the name of the decrypted property.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="PropertyNullOrEmptyException">property</exception>
        public static string ToDecryptedPropertyName(this AnonimizeService anonimize, string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new PropertyNullOrEmptyException(nameof(property));

            if (!property.StartsWith(anonimize.GetEncryptedPropertyDenominator(), StringComparison.Ordinal))
                return property;

            return property.TrimStart(anonimize.GetEncryptedPropertyDenominator().ToCharArray());
        }

        /// <summary>
        /// To the name of the encrypted property.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="PropertyNullOrEmptyException">property</exception>
        public static string ToEncryptedPropertyName(this AnonimizeService anonimize, string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new PropertyNullOrEmptyException(nameof(property));

            if (property.StartsWith(anonimize.GetEncryptedPropertyDenominator(), StringComparison.Ordinal))
                return property;

            return anonimize.GetEncryptedPropertyDenominator() + property;
        }
    }
}
