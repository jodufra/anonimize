using Xunit;
using Anonimize;
using Anonimize.Exceptions;
using Tests.Assembly;
using Tests.Assembly.Services;
using Anonimize.Services;
using System;
using Anonimize.PropertyChanged;

namespace Tests.Anonimize
{
    public class AnonimizeTest
    {
        [Fact]
        public void PropertyChangedService_ProvivesDefaultAndOverriddenService()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var service = anonimize.GetPropertyChangedService();
            Assert.NotNull(service);
            Assert.IsType(typeof(PropertyChangedService), service);

            anonimize.SetPropertyChangedService(new BasePropertyChangedService());
            service = anonimize.GetPropertyChangedService();
            Assert.NotNull(service);
            Assert.IsType(typeof(BasePropertyChangedService), service);

            anonimize.SetPropertyChangedService(null);
            service = anonimize.GetPropertyChangedService();
            Assert.NotNull(service);
            Assert.IsType(typeof(PropertyChangedService), service);
        }

        [Fact]
        public void PropertyDenominators_Null_ThrowsException()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            Assert.Throws<ArgumentNullException>(() => anonimize.SetDecryptedPropertyDenominator(null));
            Assert.Throws<ArgumentNullException>(() => anonimize.SetEncryptedPropertyDenominator(null));
            Assert.Throws<ArgumentNullException>(() => anonimize.SetPropertyDenominators(null, null));
            Assert.Throws<ArgumentNullException>(() => anonimize.SetPropertyDenominators("_", null));
            Assert.Throws<ArgumentNullException>(() => anonimize.SetPropertyDenominators(null, "_"));
        }

        [Fact]
        public void PropertyDenominators_Equal_ThrowsException()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            Assert.Throws<ArgumentOutOfRangeException>(() => anonimize.SetDecryptedPropertyDenominator(anonimize.GetEncryptedPropertyDenominator()));
            Assert.Throws<ArgumentOutOfRangeException>(() => anonimize.SetEncryptedPropertyDenominator(anonimize.GetDecryptedPropertyDenominator()));
            Assert.Throws<ArgumentOutOfRangeException>(() => anonimize.SetPropertyDenominators(string.Empty, string.Empty));
            Assert.Throws<ArgumentOutOfRangeException>(() => anonimize.SetPropertyDenominators("_", "_"));
        }

        [Fact]
        public void PropertyDenominators_ChoosesWhichPropertyIsEncryptedOrDecrypted()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithProperties), nameof(ClassWithProperties.Property1));

            anonimize.SetPropertyDenominators("_", string.Empty);
            anonimizeProperties.IsDecrypted(nameof(ClassWithProperties.Property1));
            anonimizeProperties.IsEncrypted(nameof(ClassWithProperties._Property1));

            anonimize.SetPropertyDenominators(string.Empty, "_");
            anonimizeProperties.IsDecrypted(nameof(ClassWithProperties._Property1));
            anonimizeProperties.IsEncrypted(nameof(ClassWithProperties.Property1));

            anonimize.SetDefaultPropertyDenominators();
        }


    }
}
