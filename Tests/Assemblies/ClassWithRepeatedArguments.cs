﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anonimize;
using PropertyChanged;

namespace Tests
{
    [AddINotifyPropertyChangedInterface]
    [AnonimizeProperties(typeof(ClassWithRepeatedArguments), nameof(Property1), nameof(Property1), nameof(Property2), nameof(_Property2))]
    public class ClassWithRepeatedArguments
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string _Property2 { get; set; }
    }
}
