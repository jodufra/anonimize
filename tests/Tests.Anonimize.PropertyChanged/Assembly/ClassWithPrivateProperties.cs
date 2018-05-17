using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Assembly
{
    class ClassWithPrivateProperties
    {
        public string Property1 { get; set; }
        private string _Property1 { get; set; }
        private string Property2 { get; set; }
        public string _Property2 { get; set; }
    }
}
