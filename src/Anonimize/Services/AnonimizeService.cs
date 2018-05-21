using Anonimize.Services;
using System;

namespace Anonimize.Services
{
    public class AnonimizeService
    {
        static readonly Lazy<ICryptoService> cryptoServiceDefault = new Lazy<ICryptoService>(() => new MD5TripleDESCryptoService());

        static ICryptoService cryptoServiceOverride;

        /// <summary>
        /// Gets the default lazily initialized instance of <see cref="GetCryptoService()"/>.
        /// If set to other <see cref="ICryptoService"/>, then returns the instance of that service. 
        /// </summary>
        /// <returns>
        /// The crypto service.
        /// </returns>
        public ICryptoService GetCryptoService()
        {
            return cryptoServiceOverride ?? cryptoServiceDefault.Value;
        }

        /// <summary>
        /// Sets the crypto service.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetCryptoService(ICryptoService value)
        {
            cryptoServiceOverride = value;
        }
    }
}
