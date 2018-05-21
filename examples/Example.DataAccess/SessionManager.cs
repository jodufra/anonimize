using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLib.Entities;

namespace Example
{
    public class DbContext : ConnectionEntitiesModel
    {

    }

    public static class SessionManager
    {
        static DbContext _Session;

        public static DbContext OpenSession()
        {
            if (_Session == null)
                _Session = new DbContext();
            return _Session;
        }

        static DbContext CreateSession()
        {
            return OpenSession();
        }

    }
}
