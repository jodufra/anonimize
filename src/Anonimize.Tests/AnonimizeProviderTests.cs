using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize.Tests
{
    [TestFixture]
    public class AnonimizeProviderTests
    {
        [Test]
        public void ShouldProvideAnonimizeServiceSingleton()
        {
            var instance = AnonimizeProvider.GetInstance();
            var otherInstance = AnonimizeProvider.GetInstance();

            Assert.IsNotNull(instance);
            Assert.IsNotNull(otherInstance);
            Assert.AreEqual(instance, otherInstance);
        }
    }
}
