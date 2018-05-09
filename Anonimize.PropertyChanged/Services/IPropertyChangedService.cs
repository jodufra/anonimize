using Anonimize.PropertyChanged;
using System;
using System.ComponentModel;

namespace Anonimize.Services
{
    public interface IPropertyChangedService
    {
        /// <summary>
        /// Called when [instance created] in order to set the instance event <c>PropertyChanged</c> to <c>OnPropertyChanged.</c>
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="instance">The instance.</param>
        void OnInstanceCreated<T>(T instance) where T : class, INotifyPropertyChanged;

        /// <summary>
        /// Called when [property changed] in order to encrypt or decrypt the related property that invoked the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs);

        /// <summary>
        /// Registers the specified anonimize properties.
        /// </summary>
        /// <param name="anonimizeProperties">The anonimize properties.</param>
        void Register(AnonimizeProperties anonimizeProperties);

        /// <summary>
        /// Registers the specified type and properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="properties">The properties.</param>
        void Register(Type type, params string[] properties);

        /// <summary>
        /// Registers the specified type and properties.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="instance">The type.</param>
        /// <param name="properties">The properties.</param>
        void Register<T>(T instance, Func<string[]> properties) where T : class, INotifyPropertyChanged;
    }
}