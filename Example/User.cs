using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize;

namespace Example
{
    [AnonimizeProperties(typeof(User), nameof(Name), nameof(Email), nameof(Address))]
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public User()
        {
            Anonimize.Anonimize.AnonimizeService.OnInstanceCreated(this);
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string _Name { get; set; }

        public string _Email { get; set; }

        public string _Address { get; set; }
    }
}
