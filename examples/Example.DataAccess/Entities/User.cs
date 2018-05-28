using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib.Entities
{
    public partial class User
    {
        public static void PopulateRequiredProperties(int seed, User user)
        {
            user.AccountBalance = seed * 12.345m;
            user.CivilId = seed * 333333;
            user.Email = $"{seed}@example.com";
            user.IsActive = seed % 2 == 0;
            user.Name = $"User {seed}";
        }

        public static void PopulateAllProperties(int seed, User user)
        {
            PopulateRequiredProperties(seed, user);

            user.AccountDebt = seed * 1.234m;
            user.Address = "Street nº" + seed;
            user.FiscalId = seed * 222222;
            user.IsFemale = seed % 2 == 0;
        }
    }
}
