using Xunit;
using Anonimize;
using Anonimize.Exceptions;
using Tests.Assembly;
using Tests.Assembly.Services;
using Anonimize.Services;
using System;

namespace Tests.Anonimize
{
    public class AnonimizeTest
    {
        [Fact]
        public void CryptoService_ProvivesDefaultAndOverriddenService()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(typeof(CryptoService), service);

            anonimize.SetCryptoService(new BaseCryptoService());
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(typeof(BaseCryptoService), service);

            anonimize.SetCryptoService(null);
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(typeof(CryptoService), service);
        }


    }
}
