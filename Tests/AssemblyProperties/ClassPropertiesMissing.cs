using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize;
using PropertyChanged;

namespace Tests
{
    [AddINotifyPropertyChangedInterface]
    [AnonimizeProperties(typeof(ClassPropertiesMissing), nameof(Property1))]
    public class ClassPropertiesMissing
    {
        public string Property1 { get; set; }
    }
}
