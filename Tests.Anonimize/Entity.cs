using System;
using System.Collections.Generic;
using System.Text;
using Anonimize;

namespace Tests.Anonimize
{
    [AnonimizeProperties(nameof(Name), nameof(Email))]
    public class Entity
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int Email { get; set; }

    }
}
