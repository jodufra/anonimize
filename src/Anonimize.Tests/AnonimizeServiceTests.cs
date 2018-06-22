using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize.Services;
using Anonimize.Tests.Assembly;
using NUnit.Framework;

namespace Anonimize.Tests
{
    [TestFixture]
    public class AnonimizeServiceTests
    {
        AnonimizeService anonimizeService;

        [OneTimeSetUp]
        public void SetUp()
        {
            anonimizeService = AnonimizeProvider.GetInstance();
        }

        [Test, Order(1)]
        public void ShouldProvideDefaultCryptoService()
        {
            anonimizeService.SetCryptoService(null);

            var cryptoService = anonimizeService.GetCryptoService();
            var otherCryptoService = anonimizeService.GetCryptoService();

            Assert.IsNotNull(cryptoService);
            Assert.IsInstanceOf(typeof(ICryptoService), cryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), cryptoService);
            Assert.AreEqual(cryptoService, otherCryptoService);
        }

        [Test, Order(2)]
        public void ShouldProvideOverridenCryptoService()
        {
            anonimizeService.SetCryptoService(null);

            var defaultCryptoService = anonimizeService.GetCryptoService();
            anonimizeService.SetCryptoService(new DummyCryptoService());
            var overridenCryptoService = anonimizeService.GetCryptoService();

            Assert.IsNotNull(overridenCryptoService);
            Assert.IsInstanceOf(typeof(ICryptoService), overridenCryptoService);
            Assert.IsInstanceOf(typeof(DummyCryptoService), overridenCryptoService);
            Assert.AreNotEqual(defaultCryptoService, overridenCryptoService);
        }
    }
}
