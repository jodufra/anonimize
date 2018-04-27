using Xunit;
using Anonimize;
using Anonimize.Exceptions;
using Tests.Assembly;
using Tests.Assembly.Services;
using Anonimize.Services;
using System;

namespace Tests
{
    public class AnonimizeTest
    {
        [Fact]
        public void AnonimizeService_ProvivesDefaultAndOverriddenService()
        {
            var service = Anonimize.Anonimize.AnonimizeService;
            Assert.NotNull(service);
            Assert.IsType(typeof(AnonimizeService), service);

            Anonimize.Anonimize.AnonimizeService = new BaseAnonimizeService();
            service = Anonimize.Anonimize.AnonimizeService;
            Assert.NotNull(service);
            Assert.IsType(typeof(BaseAnonimizeService), service);

            Anonimize.Anonimize.AnonimizeService = null;
            service = Anonimize.Anonimize.AnonimizeService;
            Assert.NotNull(service);
            Assert.IsType(typeof(AnonimizeService), service);
        }

        [Fact]
        public void CryptoService_ProvivesDefaultAndOverriddenService()
        {
            var service = Anonimize.Anonimize.CryptoService;
            Assert.NotNull(service);
            Assert.IsType(typeof(CryptoService), service);

            Anonimize.Anonimize.CryptoService = new BaseCryptoService();
            service = Anonimize.Anonimize.CryptoService;
            Assert.NotNull(service);
            Assert.IsType(typeof(BaseCryptoService), service);

            Anonimize.Anonimize.CryptoService = null;
            service = Anonimize.Anonimize.CryptoService;
            Assert.NotNull(service);
            Assert.IsType(typeof(CryptoService), service);
        }

        [Fact]
        public void PropertyDenominators_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Anonimize.Anonimize.DecryptedPropertyDenominator = null);
            Assert.Throws<ArgumentNullException>(() => Anonimize.Anonimize.EncryptedPropertyDenominator = null);
            Assert.Throws<ArgumentNullException>(() => Anonimize.Anonimize.SetPropertyDenominators(null, null));
            Assert.Throws<ArgumentNullException>(() => Anonimize.Anonimize.SetPropertyDenominators("_", null));
            Assert.Throws<ArgumentNullException>(() => Anonimize.Anonimize.SetPropertyDenominators(null, "_"));
        }

        [Fact]
        public void PropertyDenominators_Equal_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Anonimize.Anonimize.DecryptedPropertyDenominator = Anonimize.Anonimize.EncryptedPropertyDenominator);
            Assert.Throws<ArgumentOutOfRangeException>(() => Anonimize.Anonimize.EncryptedPropertyDenominator = Anonimize.Anonimize.DecryptedPropertyDenominator);
            Assert.Throws<ArgumentOutOfRangeException>(() => Anonimize.Anonimize.SetPropertyDenominators(string.Empty, string.Empty));
            Assert.Throws<ArgumentOutOfRangeException>(() => Anonimize.Anonimize.SetPropertyDenominators("_", "_"));
        }

        [Fact]
        public void PropertyDenominators_ChoosesWhichPropertyIsEncryptedOrDecrypted()
        {
            Anonimize.Anonimize.SetPropertyDenominators("_", string.Empty);
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithProperties), nameof(ClassWithProperties.Property1));
            anonimizeProperties.IsDecrypted(nameof(ClassWithProperties.Property1));
            anonimizeProperties.IsEncrypted(nameof(ClassWithProperties._Property1));

            Anonimize.Anonimize.SetPropertyDenominators(string.Empty, "_");
            anonimizeProperties.IsDecrypted(nameof(ClassWithProperties._Property1));
            anonimizeProperties.IsEncrypted(nameof(ClassWithProperties.Property1));

            Anonimize.Anonimize.SetDefaultPropertyDenominators();
        }


    }
}
