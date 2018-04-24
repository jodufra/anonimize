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
    [AnonimizeProperties(typeof(ClassArgumentsNull), null)]
    public class ClassArgumentsNull
    {
    }
}
