using Anonimize.Services;
using NUnit.Framework;

namespace Anonimize.Tests
{
    [TestFixture]
    public class AnonimizeServiceTests
    {
        AnonimizeService anonimizeService;

        [OneTimeSetUp]
        public void Init()
        {
            anonimizeService = AnonimizeProvider.GetInstance();
        }

        [Test]
        public void ShouldProvideDefaultAndOverridenCryptoService()
        {
            // Get the default crypto service
            var defaultCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(defaultCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), defaultCryptoService);

            var copyCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(copyCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), copyCryptoService);

            Assert.AreEqual(defaultCryptoService, copyCryptoService, "Sequenced calls to {0}.{1} should provide the same {2}", 
                typeof(AnonimizeService).Name, nameof(AnonimizeService.GetCryptoService), typeof(ICryptoService).Name);


            // Set new ICryptoService instance of the same type
            anonimizeService.SetCryptoService(new TripleDESCryptoService());
            // Get the overriden crypto service
            var overridenCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(overridenCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), overridenCryptoService);

            copyCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(copyCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), copyCryptoService);

            Assert.AreNotEqual(defaultCryptoService, overridenCryptoService);
            Assert.AreEqual(overridenCryptoService, copyCryptoService, "Sequenced calls to {0}.{1} should provide the same instance of {2}",
                typeof(AnonimizeService).Name, nameof(AnonimizeService.GetCryptoService), typeof(ICryptoService).Name);


            // Set new ICryptoService instance of a different type
            anonimizeService.SetCryptoService(new AesCryptoService());
            // Get the overriden crypto service
            overridenCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(overridenCryptoService);
            Assert.IsInstanceOf(typeof(AesCryptoService), overridenCryptoService);

            copyCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(copyCryptoService);
            Assert.IsInstanceOf(typeof(AesCryptoService), copyCryptoService);

            Assert.AreNotEqual(defaultCryptoService, overridenCryptoService);
            Assert.AreEqual(overridenCryptoService, copyCryptoService, "Sequenced calls to {0}.{1} should provide the same instance of {2}",
                typeof(AnonimizeService).Name, nameof(AnonimizeService.GetCryptoService), typeof(ICryptoService).Name);


            // Set null ICryptoService causes it to return the default service
            anonimizeService.SetCryptoService(null);
            // Get the default crypto service
            var nullCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(nullCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), nullCryptoService);

            copyCryptoService = anonimizeService.GetCryptoService();
            Assert.IsNotNull(copyCryptoService);
            Assert.IsInstanceOf(typeof(TripleDESCryptoService), copyCryptoService);

            Assert.AreNotEqual(nullCryptoService, overridenCryptoService);
            Assert.AreEqual(nullCryptoService, defaultCryptoService);
            Assert.AreEqual(nullCryptoService, copyCryptoService, "Sequenced calls to {0}.{1} should provide the same instance of {2}",
                typeof(AnonimizeService).Name, nameof(AnonimizeService.GetCryptoService), typeof(ICryptoService).Name);
        }
    }
}
