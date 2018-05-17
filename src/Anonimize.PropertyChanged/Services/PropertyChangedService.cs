using Anonimize.Exceptions;
using Anonimize.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Anonimize.Services
{
    public sealed class PropertyChangedService : IPropertyChangedService
    {
        readonly Dictionary<Type, AnonimizeProperties> typePropertiesDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedService"/> class.
        /// </summary>
        public PropertyChangedService()
        {
            typePropertiesDictionary = new Dictionary<Type, AnonimizeProperties>();
        }

        /// <summary>
        /// Called when [instance created] in order to set the instance event <c>PropertyChanged</c> to <c>OnPropertyChanged.</c>
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged" />.</typeparam>
        /// <param name="instance">The instance.</param>
        public void OnInstanceCreated<T>(T instance) where T : class, INotifyPropertyChanged
        {
            instance.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Called when [property changed] in order to encrypt or decrypt the related property that invoked the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            var type = sender.GetType();

            if (!typePropertiesDictionary.ContainsKey(type))
                return;

            var properties = typePropertiesDictionary[type];

            if (properties.IsEncrypted(eventArgs.PropertyName))
            {
                Decrypt(sender, eventArgs.PropertyName);
            }
            else if (properties.IsDecrypted(eventArgs.PropertyName))
            {
                Encrypt(sender, eventArgs.PropertyName);
            }
        }

        /// <summary>
        /// Registers the specified type and properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        /// <exception cref="TypeNullException">type</exception>
        /// <exception cref="PropertyNullException">properties</exception>
        /// <exception cref="PropertyEmptyException">properties</exception>
        public void Register(Type type, params string[] properties)
        {
            if (type == null)
                throw new TypeNullException(nameof(type));

            if (typePropertiesDictionary.ContainsKey(type))
                return;

            if (properties == null)
                throw new PropertyNullException(nameof(properties));

            if (!properties.Any())
                throw new PropertyEmptyException(nameof(properties));

            typePropertiesDictionary.Add(type, new AnonimizeProperties(type, properties));
        }

        /// <summary>
        /// Registers the specified anonimize properties.
        /// </summary>
        /// <param name="anonimizeProperties">The anonimize properties.</param>
        /// <exception cref="ArgumentNullException">anonimizeProperties</exception>
        public void Register(AnonimizeProperties anonimizeProperties)
        {
            if (anonimizeProperties == null)
                throw new ArgumentNullException(nameof(anonimizeProperties));

            if (typePropertiesDictionary.ContainsKey(anonimizeProperties.ClassType))
                return;

            typePropertiesDictionary.Add(anonimizeProperties.ClassType, anonimizeProperties);
        }

        public void Register<T>(T instance, Func<string[]> properties) where T : class, INotifyPropertyChanged
        {
            Register(instance.GetType(), properties?.Invoke());
            OnInstanceCreated(instance);
        }

        /// <summary>
        /// Gets the value of the encripted property, 
        /// decrypts the value using <see cref="AnonimizeService.GetCryptoService()"/>,
        /// sets the value to the related decrypted property
        /// </summary>
        /// <param name="obj">Object containing the <paramref name="encriptedPropertyName"/>.</param>
        /// <param name="encriptedPropertyName">Name of the encripted property.</param>
        static void Decrypt(object obj, string encriptedPropertyName)
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var input = GetValue(obj, encriptedPropertyName);
            var output = anonimize.GetCryptoService().Decrypt<string>(input);
            var propertyNameOutput = anonimize.ToDecryptedPropertyName(encriptedPropertyName);
            SetValue(obj, propertyNameOutput, output);
        }

        /// <summary>
        /// Gets the value of the decrypted property, 
        /// encripts the value using <see cref="AnonimizeService.GetCryptoService()"/>,
        /// sets the value to the related encripted property
        /// </summary>
        /// <param name="obj">Object containing the <paramref name="decriptedPropertyName"/>.</param>
        /// <param name="decriptedPropertyName">Name of the decripted property.</param>
        static void Encrypt(object obj, string decriptedPropertyName)
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var input = GetValue(obj, decriptedPropertyName);
            var output = anonimize.GetCryptoService().Encrypt(input);
            var propertyNameOutput = anonimize.ToEncryptedPropertyName(decriptedPropertyName);
            SetValue(obj, propertyNameOutput, output);
        }

        /// <summary>
        /// Gets the value of the property, named by <paramref name="propertyName"/>, of the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">Object containing the <paramref name="propertyName"/>.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The value of the property</returns>
        static string GetValue(object obj, string propertyName)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            var value = property.GetValue(obj, null);
            return (string)value;
        }

        /// <summary>
        /// Sets the value to the property, named by <paramref name="propertyName"/>, of the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">Object containing the <paramref name="propertyName"/>.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        static void SetValue(object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            property.SetValue(obj, value, null);
        }
    }

}
