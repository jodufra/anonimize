using System;
using System.Security.Cryptography;

namespace Anonimize.Services
{
    public sealed class TripleDESCryptoService : BaseSymmetricCryptoService
    {
        protected override int IvLength => 8;
        protected override int KeyLength => 16;

        /// <summary>
        /// Decrypts the specified input using <see cref="TripleDESCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The decripted input</returns>
        public override byte[] Decrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = GetIV();
                des.Key = GetKey();
                des.Padding = PaddingMode.PKCS7;
                outputBuffer = des.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            return outputBuffer;
        }

        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The decrypted input</returns>
        public override T Decrypt<T>(string input)
        {
            try
            {
                var inputBuffer = Convert.FromBase64String(input);
                var outputBuffer = Decrypt(inputBuffer);
                return Deserialize<T>(outputBuffer);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Encrypts the specified input using <see cref="TripleDESCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The encrypted input</returns>
        public override byte[] Encrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = GetIV();
                des.Key = GetKey();
                des.Padding = PaddingMode.PKCS7;
                outputBuffer = des.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            return outputBuffer;
        }

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The encrypted input</returns>
        public override string Encrypt<T>(T input)
        {
            try
            {
                var inputBuffer = Serialize(input);
                var outputBuffer = Encrypt(inputBuffer);
                return Convert.ToBase64String(outputBuffer);
            }
            catch (Exception)
            {
                return default(string);
            }
        }
    }
}
