using System;
using System.Security.Cryptography;
using System.Text;

namespace Anonimize.Services
{
    public abstract class BaseSymmetricCryptoService : BaseCryptoService
    {
        protected virtual int IvLength => 8;
        protected virtual int KeyLength => 16;
        protected virtual int DeriveIterations => 1000;

        string iv = "Anonimize:Iv";
        string key = "Anonimize:Key";
        byte[] ivEncrypted;
        byte[] keyEncrypted;

        protected byte[] GetIV()
        {
            if (ivEncrypted == null)
                ivEncrypted = GenerateEncryptedBytes(iv, DeriveIterations, IvLength);

            return ivEncrypted;
        }

        /// <summary>
        /// Sets the triple DES IV.
        /// </summary>
        /// <param name="value">
        /// The triple DES iv.
        /// </param>
        public void SetIV(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("IV can't be null or empty.");

            iv = value;
            ivEncrypted = null;
        }

        /// <summary>
        /// Sets the triple DES IV.
        /// </summary>
        /// <param name="value">
        /// The triple DES iv.
        /// </param>
        public void SetIV(byte[] value)
        {
            if (value == null || value.Length != 8)
                throw new ArgumentException("Expecting byte[] of length 8.");

            iv = string.Empty;
            ivEncrypted = value;
        }

        protected byte[] GetKey()
        {
            if (keyEncrypted == null)
                keyEncrypted = GenerateEncryptedBytes(key, DeriveIterations, KeyLength);

            return keyEncrypted;
        }

        /// <summary>
        /// Sets the triple DES Key.
        /// </summary>
        /// <param name="value">
        /// The triple DES key.
        /// </param>
        public void SetKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Key can't be null or empty.");

            key = value;
            keyEncrypted = null;
        }

        /// <summary>
        /// Sets the triple DES Key.
        /// </summary>
        /// <param name="value">
        /// The triple DES key.
        /// </param>
        public void SetKey(byte[] value)
        {
            if (value == null || value.Length != 8)
                throw new ArgumentException("Expecting byte[] of length 8.");

            key = string.Empty;
            keyEncrypted = value;
        }

        static byte[] GenerateEncryptedBytes(string input, int iterations, int outputLength)
        {
            var inputBuffer = Encoding.ASCII.GetBytes(input);
            byte[] salt;

            using (var md5 = new MD5CryptoServiceProvider())
            {
                salt = md5.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }

            using (var rfc = new Rfc2898DeriveBytes(input, salt, iterations))
            {
                return rfc.GetBytes(outputLength);
            }
        }
    }
}
