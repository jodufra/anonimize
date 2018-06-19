using System;
using System.Security.Cryptography;
using System.Text;

namespace Anonimize.Services
{
    public abstract class BaseSymmetricCryptoService : BaseCryptoService
    {
        protected virtual int IvLength => 8;
        protected virtual int KeyLength => 16;
        protected virtual int RfcIterartions => 1000;

        string iv = "Anonimize:Iv";
        string key = "Anonimize:Key";
        byte[] ivEncrypted = null;
        byte[] keyEncrypted = null;

        protected byte[] GetIV()
        {
            if (ivEncrypted == null)
            {
                var buffer = Encoding.ASCII.GetBytes(iv);
                using (var rfc = new Rfc2898DeriveBytes(buffer, buffer, RfcIterartions))
                {
                    ivEncrypted = rfc.GetBytes(IvLength);
                }
            }
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
            {
                var buffer = Encoding.ASCII.GetBytes(key);
                using (var rfc = new Rfc2898DeriveBytes(buffer, buffer, RfcIterartions))
                {
                    keyEncrypted = rfc.GetBytes(KeyLength);
                }
            }
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
    }
}
