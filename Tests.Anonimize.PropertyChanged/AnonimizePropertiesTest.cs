using Xunit;
using Anonimize.Exceptions;
using Tests.Assembly;
using Anonimize.PropertyChanged;

namespace Tests.Anonimize
{
    public class AnonimizePropertiesTest
    {
        [Fact]
        public void Contructor_NullType_ThrowsException()
        {
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null));
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null, null));
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null, ""));
        }

        [Fact]
        public void Contructor_NullProperties_ThrowsException()
        {
            Assert.Throws<PropertyNullException>(() => new AnonimizeProperties(typeof(ClassBase), null));
        }

        [Fact]
        public void AddProperty_MissingProperties_ThrowsException()
        {
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithMissingProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithMissingProperties.Property1)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithMissingProperties._Property2)));
        }

        [Fact]
        public void AddProperty_PrivateProperties_ThrowsException()
        {
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithPrivateProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithPrivateProperties.Property1)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithPrivateProperties._Property2)));
        }

        [Fact]
        public void AddProperty_NonStringProperties_ThrowsException()
        {
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithNonStringProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyStruct)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyEnum)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyInterface)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyClass)));
        }

        [Fact]
        public void AddProperty_RegistersEncryptedAndDecryptedProperty()
        {
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithProperties));

            // Add decrypted property
            anonimizeProperties.AddProperty(nameof(ClassWithProperties.Property1));
            Assert.True(anonimizeProperties.IsDecrypted(nameof(ClassWithProperties.Property1)));
            Assert.True(anonimizeProperties.IsEncrypted(nameof(ClassWithProperties._Property1)));
            Assert.True(anonimizeProperties.DecryptedProperties.Contains(nameof(ClassWithProperties.Property1)));
            Assert.True(anonimizeProperties.EncryptedProperties.Contains(nameof(ClassWithProperties._Property1)));

            // Add encrypted property
            anonimizeProperties.AddProperty(nameof(ClassWithProperties._Property2));
            Assert.True(anonimizeProperties.IsDecrypted(nameof(ClassWithProperties.Property2)));
            Assert.True(anonimizeProperties.IsEncrypted(nameof(ClassWithProperties._Property2)));
            Assert.True(anonimizeProperties.DecryptedProperties.Contains(nameof(ClassWithProperties.Property2)));
            Assert.True(anonimizeProperties.EncryptedProperties.Contains(nameof(ClassWithProperties._Property2)));
        }
    }
}
