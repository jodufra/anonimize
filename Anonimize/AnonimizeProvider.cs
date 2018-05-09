using Anonimize.Services;
using System;

namespace Anonimize
{
    public static class AnonimizeProvider
    {
        static readonly Lazy<AnonimizeService> anonimize = new Lazy<AnonimizeService>(() => new AnonimizeService());

        /// <summary>
        /// Gets the lazily initialized instance of <see cref="AnonimizeService"/>.
        /// </summary>
        /// <returns></returns>
        public static AnonimizeService GetInstance() => anonimize.Value;
    }
}
