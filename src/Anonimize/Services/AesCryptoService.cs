using System;
using System.Security.Cryptography;

namespace Anonimize.Services
{
    public sealed class AesCryptoService : BaseSymmetricCryptoService
    {
        protected override int IvLength => 16;
        protected override int KeyLength => 32;

        /// <summary>
        /// Decrypts the specified input using <see cref="AesCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The decripted input</returns>
        public override byte[] Decrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.IV = GetIV();
                aes.Key = GetKey();
                aes.Padding = PaddingMode.PKCS7;
                outputBuffer = aes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
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
        /// Encrypts the specified input using <see cref="AesCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The encrypted input</returns>
        public override byte[] Encrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.IV = GetIV();
                aes.Key = GetKey();
                aes.Padding = PaddingMode.PKCS7;
                outputBuffer = aes.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
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
