using Anonimize.Exceptions;
using Anonimize.Services;
using System;

namespace Anonimize
{
    public static class AnonimizeExtensions
    {
        static readonly Lazy<ICryptoService> cryptoServiceDefault = new Lazy<ICryptoService>(() => new CryptoService());

        static ICryptoService cryptoServiceOverride;

        /// <summary>
        /// Gets the default lazily initialized instance of <see cref="GetCryptoService()"/>.
        /// If set to other <see cref="ICryptoService"/>, then returns the instance of that service. 
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <returns>
        /// The crypto service.
        /// </returns>
        public static ICryptoService GetCryptoService(this AnonimizeService anonimize)
        {
            return cryptoServiceOverride ?? cryptoServiceDefault.Value;
        }

        /// <summary>
        /// Sets the crypto service.
        /// </summary>
        /// <param name="anonimize">The anonimize.</param>
        /// <param name="value">The value.</param>
        public static void SetCryptoService(this AnonimizeService anonimize, ICryptoService value)
        {
            cryptoServiceOverride = value;
        }
    }
}
