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
    [TestFixture(typeof(bool?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(byte?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(DateTime?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(decimal?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(double?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(Int16?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(Int32?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(Int64?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(float?), typeof(TripleDESCryptoService))]
    [TestFixture(typeof(string), typeof(TripleDESCryptoService))]
    //[TestFixture(typeof(bool?), typeof(AesCryptoService))]
    //[TestFixture(typeof(byte?), typeof(AesCryptoService))]
    //[TestFixture(typeof(DateTime?), typeof(AesCryptoService))]
    //[TestFixture(typeof(decimal?), typeof(AesCryptoService))]
    //[TestFixture(typeof(double?), typeof(AesCryptoService))]
    //[TestFixture(typeof(Int16?), typeof(AesCryptoService))]
    //[TestFixture(typeof(Int32?), typeof(AesCryptoService))]
    //[TestFixture(typeof(Int64?), typeof(AesCryptoService))]
    //[TestFixture(typeof(float?), typeof(AesCryptoService))]
    //[TestFixture(typeof(string), typeof(AesCryptoService))]
    public class CryptoServiceTests<T, TCrypto> where TCrypto : ICryptoService, new()
    {
        TCrypto cryptoService;

        [Datapoint]
        public bool?[] Booleans = { null, false, true };
        [Datapoint]
        public byte?[] Bytes = { null, byte.MinValue, 0, byte.MaxValue };
        [Datapoint]
        public DateTime?[] DateTimes = { null, DateTime.MinValue, DateTime.MaxValue };
        [Datapoint]
        public decimal?[] Decimals = { null, decimal.MinValue, 0m, decimal.MaxValue };
        [Datapoint]
        public double?[] Doubles = { null, double.MinValue, 0d, double.MaxValue };
        [Datapoint]
        public Int16?[] Integers16 = { null, Int16.MinValue, 0, Int16.MaxValue };
        [Datapoint]
        public Int32?[] Integers32 = { null, Int32.MinValue, 0, Int32.MaxValue };
        [Datapoint]
        public Int64?[] Integers64 = { null, Int64.MinValue, 0, Int64.MaxValue };
        [Datapoint]
        public float?[] Singles = { null, float.MinValue, 0f, float.MaxValue };
        [Datapoint]
        public string[] Strings = { null, string.Empty, " " };

        [OneTimeSetUp]
        public void SetUp()
        {
            cryptoService = new TCrypto();
        }

        [Theory]
        public void DecryptedValueMustBeEqualToInputValue(T input)
        {
            var encryptedValue = cryptoService.Encrypt(input);
            var decryptedValue = cryptoService.Decrypt<T>(encryptedValue);
            Assert.AreEqual(input, decryptedValue);
        }
    }
}
