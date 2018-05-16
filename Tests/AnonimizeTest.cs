using Xunit;
using Anonimize;
using Tests.Assembly.Services;
using Anonimize.Services;

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
            Assert.IsType(typeof(MD5TripleDESCryptoService), service);

            anonimize.SetCryptoService(new BaseCryptoService());
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(typeof(BaseCryptoService), service);

            anonimize.SetCryptoService(null);
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(typeof(MD5TripleDESCryptoService), service);
        }


    }
}
