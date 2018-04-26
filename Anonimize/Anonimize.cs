using Anonimize.Exceptions;
using Anonimize.Services;
using System;

namespace Anonimize
{
    /// <summary>
    /// 
    /// </summary>
    public static class Anonimize
    {
        static readonly Lazy<IAnonimizeService> anonimizeServiceDefault = new Lazy<IAnonimizeService>(() => new AnonimizeService());

        static IAnonimizeService anonimizeServiceOverride;

        static readonly Lazy<ICryptoService> cryptoServiceDefault = new Lazy<ICryptoService>(() => new CryptoService());

        static ICryptoService cryptoServiceOverride;

        static string encryptedPropertyDenominator = "_";

        static string decryptedPropertyDenominator = string.Empty;

        /// <summary>
        /// Gets the default lazily initialized instance of <see cref="AnonimizeService"/>.
        /// If set to other <see cref="IAnonimizeService"/>, then returns the instance of that service. 
        /// </summary>
        /// <value>
        /// The anonimize service.
        /// </value>
        public static IAnonimizeService AnonimizeService
        {
            get => anonimizeServiceOverride ?? anonimizeServiceDefault.Value;
            set => anonimizeServiceOverride = value;
        }

        /// <summary>
        /// Gets the default lazily initialized instance of <see cref="CryptoService"/>.
        /// If set to other <see cref="ICryptoService"/>, then returns the instance of that service. 
        /// </summary>
        /// <value>
        /// The crypto service.
        /// </value>
        public static ICryptoService CryptoService
        {
            get => cryptoServiceOverride ?? cryptoServiceDefault.Value;
            set => cryptoServiceOverride = value;
        }

        /// <summary>
        /// Gets or sets the starting denominator of the encrypted properties. Default <c>_</c>.
        /// </summary>
        /// <example>
        /// If value is _ then the related encrypted property of <c>Name</c> is <c>_Name</c>
        /// </example>
        /// <value>
        /// The encrypted property denominator.
        /// </value>
        public static string EncryptedPropertyDenominator
        {
            get => encryptedPropertyDenominator;
            set => SetPropertyDenominators(value, decryptedPropertyDenominator);
        }

        /// <summary>
        /// Gets or sets the starting denominator of the decrypted properties. Default <see cref="string.Empty"/>.
        /// </summary>
        /// <example>
        /// If value is <see cref="string.Empty"/> then the related decrypted property of <c>_Name</c> is <c>Name</c>
        /// </example>
        /// <value>
        /// The decrypted property denominator.
        /// </value>
        public static string DecryptedPropertyDenominator
        {
            get => decryptedPropertyDenominator;
            set => SetPropertyDenominators(encryptedPropertyDenominator, value);
        }

        /// <summary>
        /// Sets the property denominators.
        /// </summary>
        /// <param name="encryptedPropertyDenominator">The encrypted property denominator.</param>
        /// <param name="decryptedPropertyDenominator">The decrypted property denominator.</param>
        /// <exception cref="ArgumentNullException">
        /// encryptedPropertyDenominator is null
        /// or
        /// decryptedPropertyDenominator is null
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">encryptedPropertyDenominator equals decryptedPropertyDenominator</exception>
        public static void SetPropertyDenominators(string encryptedPropertyDenominator, string decryptedPropertyDenominator)
        {
            if (encryptedPropertyDenominator == null)
                throw new ArgumentNullException(nameof(encryptedPropertyDenominator));

            if (decryptedPropertyDenominator == null)
                throw new ArgumentNullException(nameof(decryptedPropertyDenominator));

            if (encryptedPropertyDenominator == decryptedPropertyDenominator)
                throw new ArgumentOutOfRangeException();

            Anonimize.encryptedPropertyDenominator = encryptedPropertyDenominator;
            Anonimize.decryptedPropertyDenominator = decryptedPropertyDenominator;
        }

        /// <summary>
        /// To the name of the decrypted property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="PropertyNullOrEmptyException">property</exception>
        public static string ToDecryptedPropertyName(string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new PropertyNullOrEmptyException(nameof(property));

            if (!property.StartsWith(EncryptedPropertyDenominator, StringComparison.Ordinal))
                return property;

            return property.TrimStart(EncryptedPropertyDenominator.ToCharArray());
        }

        /// <summary>
        /// To the name of the encrypted property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="PropertyNullOrEmptyException">property</exception>
        public static string ToEncryptedPropertyName(string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new PropertyNullOrEmptyException(nameof(property));

            if (property.StartsWith(EncryptedPropertyDenominator, StringComparison.Ordinal))
                return property;

            return EncryptedPropertyDenominator + property;
        }
    }
}
