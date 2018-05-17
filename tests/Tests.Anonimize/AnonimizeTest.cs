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
            var serviceTypeDefault = typeof(MD5TripleDESCryptoService);
            var serviceTypeOverriden = typeof(BaseCryptoService);
             
            var anonimize = AnonimizeProvider.GetInstance();
            var service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(serviceTypeDefault, service);

            anonimize.SetCryptoService(new BaseCryptoService());
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(serviceTypeOverriden, service);

            anonimize.SetCryptoService(null);
            service = anonimize.GetCryptoService();
            Assert.NotNull(service);
            Assert.IsType(serviceTypeDefault, service);
        }
    }
}
