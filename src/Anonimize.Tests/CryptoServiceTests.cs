using System;
using System.Collections.Generic;
using Anonimize.Services;
using NUnit.Framework;

namespace Anonimize.Tests
{
    public abstract class CryptoServiceTests<T>
    {
        readonly ICryptoService cryptoService;

        protected CryptoServiceTests(ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

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

        public virtual void DecryptedValueMustBeEqualToInputValue(T[] inputs)
        {
            foreach (var input in inputs)
            {
                var encryptedValue = cryptoService.Encrypt(input);
                var decryptedValue = cryptoService.Decrypt<T>(encryptedValue);
                Assert.AreEqual(input, decryptedValue);
            }
        }
    }
}
