using System;
using System.ComponentModel;
using Anonimize.Services;
using Anonimize.PropertyChanged;

namespace Tests.Assembly.Services
{
    class BasePropertyChangedService : IPropertyChangedService
    {
        public void OnInstanceCreated<T>(T instance) where T : class, INotifyPropertyChanged
        {
            throw new NotImplementedException();
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public void Register(AnonimizeProperties anonimizeProperties)
        {
            throw new NotImplementedException();
        }

        public void Register(Type type, params string[] properties)
        {
            throw new NotImplementedException();
        }

        public void Register<T>(T instance, Func<string[]> properties) where T : class, INotifyPropertyChanged
        {
            throw new NotImplementedException();
        }
    }
}
