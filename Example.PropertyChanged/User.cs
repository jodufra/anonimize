using Anonimize;
using PropertyChanged;
using System.ComponentModel;

namespace Example
{
    [AddINotifyPropertyChangedInterface]
    public class Person
    {
        public Person()
        {
            var anonimize = AnonimizeProvider.GetInstance();
            var service = anonimize.GetPropertyChangedService();            
            service.Register((INotifyPropertyChanged)this, () => new string[]{
                nameof(Name),
                nameof(Email),
                nameof(Address)
            });
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string _Name { get; set; }

        public string _Email { get; set; }

        public string _Address { get; set; }
    }
}
