using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.ViewModels.Account;

namespace TimeTravelAgency.Domain.Helpers
{
    public class DataGenerator
    {
        Faker<AccountGeneratedViewModel> accountModelFake;

        public DataGenerator()
        {
            Randomizer.Seed = new Random();

            accountModelFake = new Faker<AccountGeneratedViewModel>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 10000))
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.ULogin, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.HashPassword, f => HashPasswordHelper.HashPassowrd(f.Internet.Password()))
                .RuleFor(u => u.URole, f => Enum.Role.User)
                .RuleFor(u => u.Age, f => f.Random.Int(18, 100))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Uaddress, f => f.Address.FullAddress());
        }

        public AccountGeneratedViewModel GenerateAccount()
        {
            return accountModelFake.Generate();
        }

        public IEnumerable<AccountGeneratedViewModel> GenerateAccounts()
        {
            return accountModelFake.GenerateForever();
        }
    }
}
