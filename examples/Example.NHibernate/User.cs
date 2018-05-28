using System;

namespace Example
{
    public class User
    {
        public virtual Decimal AccountBalance { get; set; }
        public virtual Decimal? AccountDebt { get; set; }
        public virtual string Address { get; set; }
        public virtual int CivilId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime? DateUpdated { get; set; }
        public virtual string Email { get; set; }
        public virtual int? FiscalId { get; set; }
        public virtual int Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool? IsFemale { get; set; }
        public virtual string Name { get; set; }

        public static void PopulateAllProperties(int seed, User user)
        {
            PopulateRequiredProperties(seed, user);

            user.AccountDebt = seed * 1.234m;
            user.Address = "Street nº" + seed;
            user.FiscalId = seed * 222222;
            user.IsFemale = seed % 2 == 0;
        }

        public static void PopulateRequiredProperties(int seed, User user)
        {
            user.AccountBalance = seed * 12.345m;
            user.CivilId = seed * 333333;
            user.Email = $"{seed}@example.com";
            user.IsActive = seed % 2 == 0;
            user.Name = $"User {seed}";
        }
    }
}