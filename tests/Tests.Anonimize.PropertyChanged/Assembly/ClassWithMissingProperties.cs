using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Assembly
{
    class ClassWithMissingProperties
    {
        public string Property1 { get; set; }
        public string _Property2 { get; set; }
    }
}
