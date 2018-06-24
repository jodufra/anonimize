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
            Assert.IsNotNull(instance);

            var otherInstance = AnonimizeProvider.GetInstance();
            Assert.IsNotNull(otherInstance);

            Assert.AreEqual(instance, otherInstance, "Sequenced calls to {0}.{1} should provide the same {2}",
                typeof(AnonimizeProvider).Name, nameof(AnonimizeProvider.GetInstance), typeof(Services.AnonimizeService).Name);
        }
    }
}
