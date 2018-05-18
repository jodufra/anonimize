﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Anonimize.Services
{
    public sealed class MD5TripleDESCryptoService : ICryptoService
    {
        static byte[] iv = { 125, 6, 87, 63, 172, 2, 173, 69 };

        static string key = "Anonimize:Key";

        /// <summary>
        /// Gets or sets the triple DES iv.
        /// </summary>
        /// <value>
        /// The triple DES iv.
        /// </value>
        public static byte[] IV
        {
            get => iv;
            set => iv = value;
        }

        /// <summary>
        /// Gets or sets the triple DES key.
        /// </summary>
        /// <value>
        /// The triple DES key.
        /// </value>
        public static string Key
        {
            get => key;
            set => key = value;
        }

        /// <summary>
        /// Decrypts the specified input using <see cref="MD5CryptoServiceProvider"/> and <see cref="TripleDESCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The decripted input</returns>
        public byte[] Decrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = IV;
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    des.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(Key));
                }

                outputBuffer = des.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            return outputBuffer;
        }

        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The decrypted input</returns>
        public T Decrypt<T>(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return default(T);
            
            byte[] inputBuffer;

            try
            {
                inputBuffer = Convert.FromBase64String(input);
            }
            catch (Exception)
            {
                return default(T);
            }

            var outputBuffer = Decrypt(inputBuffer);

            try
            {
                return Deserialize<T>(outputBuffer);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Encrypts the specified input using <see cref="MD5CryptoServiceProvider"/> and <see cref="TripleDESCryptoServiceProvider"/>.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns>The encrypted input</returns>
        public byte[] Encrypt(byte[] inputBuffer)
        {
            byte[] outputBuffer;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.IV = IV;
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    des.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(Key));
                }

                outputBuffer = des.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            return outputBuffer;
        }

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The encrypted input</returns>
        public string Encrypt<T>(T input)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
                return default(string);

            byte[] inputBuffer;

            try
            {
                inputBuffer = Serialize(input);
            }
            catch (Exception)
            {
                return default(string);
            }

            var outputBuffer = Encrypt(inputBuffer);

            try
            {
                return Convert.ToBase64String(outputBuffer);
            }
            catch (Exception)
            {
                return default(string);
            }
        }

        static byte[] Serialize<T>(T param)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, param);
                return stream.ToArray();
            }
        }

        static T Deserialize<T>(byte[] param)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(param))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
