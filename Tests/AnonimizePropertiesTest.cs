using System;
using Xunit;
using Anonimize;
using Anonimize.Exceptions;
using PropertyChanged;

namespace Tests
{
    public class AnonimizePropertiesTest
    {
        [Fact]
        public void ContructorTypeNullThrowsException()
        {
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null));
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null, null));
            Assert.Throws<TypeNullException>(() => new AnonimizeProperties(null, ""));
        }

        [Fact]
        public void ContructorPropertiesNullThrowsException()
        {
            Assert.Throws<PropertyNullException>(() => new AnonimizeProperties(typeof(ClassBase), null));
        }

        [Fact]
        public void AddMissingPropertiesThrowsException()
        {
            var baseClass = new ClassWithMissingProperties();
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithMissingProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithMissingProperties.Property1)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithMissingProperties._Property2)));
        }

        [Fact]
        public void AddPrivatePropertiesThrowsException()
        {
            var baseClass = new ClassWithPrivateProperties();
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithPrivateProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithPrivateProperties.Property1)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithPrivateProperties._Property2)));
        }

        [Fact]
        public void AddNonStringPropertiesThrowsException()
        {
            var baseClass = new ClassWithNonStringProperties();
            var anonimizeProperties = new AnonimizeProperties(typeof(ClassWithNonStringProperties));

            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyStruct)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyEnum)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyInterface)));
            Assert.Throws<PropertyMissingException>(() => anonimizeProperties.AddProperty(nameof(ClassWithNonStringProperties.PropertyClass)));
        }

        enum EBase { }

        interface IBase { }

        struct SBase { }

        class ClassBase
        {
            public string Property1 { get; set; }
            public string _Property1 { get; set; }
            public string Property2 { get; set; }
            public string _Property2 { get; set; }
        }

        class ClassWithMissingProperties
        {
            public string Property1 { get; set; }
            public string _Property2 { get; set; }
        }

        class ClassWithPrivateProperties
        {
            public string Property1 { get; set; }
            private string _Property1 { get; set; }
            private string Property2 { get; set; }
            public string _Property2 { get; set; }
        }

        class ClassWithNonStringProperties
        {
            public SBase PropertyStruct { get; set; }
            public SBase _PropertyStruct { get; set; }
            public EBase PropertyEnum { get; set; }
            public EBase _PropertyEnum { get; set; }
            public IBase PropertyInterface { get; set; }
            public IBase _PropertyInterface { get; set; }
            public ClassBase PropertyClass { get; set; }
            public ClassBase _PropertyClass { get; set; }
        }
    }
}
