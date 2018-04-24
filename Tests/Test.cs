using System;
using Xunit;
using Anonimize;
using PropertyChanged;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void ArgumentsNullThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AnonimizePropertiesAttribute(typeof(BaseClass), null));
        }

        [Fact]
        public void ArgumentsEmptyThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AnonimizePropertiesAttribute(typeof(BaseClass), string.Empty));
        }

        [Fact]
        public void ArgumentsWhiteSpaceThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AnonimizePropertiesAttribute(typeof(BaseClass), " "));
            Assert.Throws<ArgumentOutOfRangeException>(() => new AnonimizePropertiesAttribute(typeof(BaseClass), "    "));
        }

        [Fact]
        public void PropertiesMissingThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AnonimizePropertiesAttribute(typeof(BaseClass), "Property1"));
        }

        [AddINotifyPropertyChangedInterface]
        public class BaseClass { }
    }
}
