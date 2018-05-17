using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Assembly
{
    class ClassWithNonStringProperties
    {
        public SBase PropertyStruct { get; set; }
        public SBase _PropertyStruct { get; set; }
        public EBase PropertyEnum { get; set; }
        public EBase _PropertyEnum { get; set; }
        public IBase PropertyInterface { get; set; }
        public IBase _PropertyInterface { get; set; }
        public ClassBase PropertyClass { get; set; }
        public ClassBase _PropertyClass { get; set; }
    }
}
