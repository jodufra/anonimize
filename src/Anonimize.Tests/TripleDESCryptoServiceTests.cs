using System;
using System.Collections.Generic;
using Anonimize.Services;
using NUnit.Framework;

namespace Anonimize.Tests
{
    [TestFixture(typeof(bool?))]
    [TestFixture(typeof(byte?))]
    [TestFixture(typeof(DateTime?))]
    [TestFixture(typeof(decimal?))]
    [TestFixture(typeof(double?))]
    [TestFixture(typeof(Int16?))]
    [TestFixture(typeof(Int32?))]
    [TestFixture(typeof(Int64?))]
    [TestFixture(typeof(float?))]
    [TestFixture(typeof(string))]
    [Category("CryptoService")]
    public class TripleDESCryptoServiceTests<T> : CryptoServiceTests<T>
    {
        public TripleDESCryptoServiceTests() : base(new TripleDESCryptoService()) { }

        [Theory]
        public override void DecryptedValueMustBeEqualToInputValue(T[] inputs)
        {
            base.DecryptedValueMustBeEqualToInputValue(inputs);
        }        
    }
}
