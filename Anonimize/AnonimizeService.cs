using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonimize
{
    public static class AnonimizeService
    {
        static readonly Dictionary<Type, AnonimizePropertiesAttribute> attributeDictionary;

        static AnonimizeService()
        {
            attributeDictionary = new Dictionary<Type, AnonimizePropertiesAttribute>();
        }

        public static void Register<T>(T instance) where T : class, INotifyPropertyChanged
        {
            var type = typeof(T);
            var attribute = (AnonimizePropertiesAttribute)Attribute.GetCustomAttribute(type, typeof(AnonimizePropertiesAttribute));

            if (attribute == null)
                return;

            if (!attributeDictionary.ContainsKey(type))
                attributeDictionary.Add(type, attribute);

            instance.PropertyChanged += OnPropertyChanged;
        }

        public static void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            var type = sender.GetType();

            if (!attributeDictionary.ContainsKey(type))
                return;

            var attribute = attributeDictionary[type];

            if (attribute.IsEncryptable(eventArgs.PropertyName))
            {
                Decrypt(sender, eventArgs.PropertyName);
            }
            else if (attribute.IsDecryptable(eventArgs.PropertyName))
            {
                Encrypt(sender, eventArgs.PropertyName);
            }
        }

        public static string ToDecryptablePropertyName(string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                throw new ArgumentOutOfRangeException(nameof(property), $"Property name mustn't be null, or empty or only white spaces.");

            if (!property.StartsWith("_", StringComparison.Ordinal))
                return property;

            return property.TrimStart('_');
        }

        public static string ToEncryptablePropertyName(string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                throw new ArgumentOutOfRangeException(nameof(property), $"Property name mustn't be null, or empty or only white spaces.");

            if (property.StartsWith("_", StringComparison.Ordinal))
                return property;

            return $"_{property}";
        }

        static void Encrypt(object sender, string propertyName)
        {
            var input = GetValue<string>(sender, propertyName);
            var output = CryptoService.Encrypt(input);
            var propertyNameOutput = ToEncryptablePropertyName(propertyName);
            SetValue(sender, propertyNameOutput, output);
        }

        static void Decrypt(object sender, string propertyName)
        {
            var input = GetValue<string>(sender, propertyName);
            var output = CryptoService.Encrypt(input);
            var propertyNameOutput = ToDecryptablePropertyName(propertyName);
            SetValue(sender, propertyNameOutput, output);
        }

        static T GetValue<T>(object sender, string propertyName)
        {
            var type = sender.GetType();
            var property = type.GetProperty(propertyName);
            var value = property.GetValue(sender, null);
            return (T)value;
        }

        static void SetValue(object sender, string propertyName, object value)
        {
            var type = sender.GetType();
            var property = type.GetProperty(propertyName);
            property.SetValue(sender, value, null);
        }
    }
}
