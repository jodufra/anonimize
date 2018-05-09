using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize;
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
    }
}
