using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize.Services
{
    public abstract class BaseCryptoService : ICryptoService
    {
        public abstract byte[] Decrypt(byte[] inputBuffer);

        public abstract T Decrypt<T>(string input);

        public abstract byte[] Encrypt(byte[] inputBuffer);

        public abstract string Encrypt<T>(T input);

        protected byte[] Serialize<T>(T param)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, param);
                return stream.ToArray();
            }
        }

        protected T Deserialize<T>(byte[] param)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(param))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
