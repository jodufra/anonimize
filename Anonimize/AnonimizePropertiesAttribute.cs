using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize
{
    /// <summary>Represents the base class for custom attributes.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AnonimizePropertiesAttribute : AddINotifyPropertyChangedInterfaceAttribute
    {
        readonly HashSet<string> encryptedProperties;
        readonly HashSet<string> decryptedProperties;

        public AnonimizePropertiesAttribute(params string[] properties)
        {
            encryptedProperties = new HashSet<string>();
            decryptedProperties = new HashSet<string>();

            foreach (var property in properties)
            {
                var isEncrypted = property.StartsWith("_", StringComparison.Ordinal);
                var encryptedProperty = isEncrypted ? property : $"_{property}";
                var decryptedProperty = isEncrypted ? property.TrimStart('_') : property;

                encryptedProperties.Add(encryptedProperty);
                decryptedProperties.Add(decryptedProperty);
            }
        }

        public bool IsEncryptedProperty(string property)
        {
            return encryptedProperties.Contains(property);
        }

        public bool IsDecryptedProperty(string property)
        {
            return decryptedProperties.Contains(property);
        }
    }
}