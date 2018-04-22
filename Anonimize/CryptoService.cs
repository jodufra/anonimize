using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize
{
    public static class CryptoService
    {
        public static string TRIPLE_DES_KEY = "App:Anonimize:Key";

        public static byte[] TRIPLE_DES_IV = { 125, 6, 87, 63, 172, 2, 173, 69 };

        /// <summary>Decrypts the provided input using MD5 and TripleDES CryptoServices</summary>
        /// <param name="input">The input string to be decrypted</param>
        /// <returns>The decrypted input or empty if the input is not decryptable</returns>
        public static string Decrypt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            byte[] inputBuffer;
            byte[] outputBuffer;

            try
            {
                inputBuffer = Convert.FromBase64String(input);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = TRIPLE_DES_IV;
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    des.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(TRIPLE_DES_KEY));
                }

                outputBuffer = des.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }

            try
            {
                return Encoding.ASCII.GetString(outputBuffer);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>Encrypts the provided input using MD5 and TripleDES CryptoServices</summary>
        /// <param name="input">The input string to be encrypted</param>
        /// <returns>The decrypted input or empty if the input is not encryptable</returns>
        public static string Encrypt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            byte[] inputBuffer;
            byte[] outputBuffer;

            try
            {
                inputBuffer = Encoding.ASCII.GetBytes(input);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = TRIPLE_DES_IV;
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    des.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(TRIPLE_DES_KEY));
                }

                outputBuffer = des.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }

            try
            {
                return Convert.ToBase64String(outputBuffer);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
