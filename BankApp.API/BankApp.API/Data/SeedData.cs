using BankApp.API.Models;

namespace BankApp.API.Data
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            DataContext context = serviceProvider.GetRequiredService<DataContext>();

            if (context.Customers.Any())
            {
                context.Customers.RemoveRange(context.Customers);
            }

            if (context.Addresses.Any())
            {
                context.Addresses.RemoveRange(context.Addresses);
            }

            if (context.Accounts.Any())
            {
                context.Accounts.RemoveRange(context.Accounts);
            }

            context.SaveChanges();

            var addresses = new List<Address>
            {
                new Address { Number = "44", Street = "Main St", City = "London", PostCode = "W1D 1LU", Country = "UK" },
                new Address { Number = "7", Street = "Oxford St", City = "Manchester", PostCode = "M1 5AN", Country = "UK" },
                new Address { Number = "15", Street = "Market St", City = "San Francisco", PostCode = "94105", Country = "USA" },
                new Address { Number = "8", Street = "Queen St", City = "Toronto", PostCode = "M5C 1S6", Country = "Canada" },
                new Address { Number = "10", Street = "High Street", City = "Birmingham", PostCode = "B4 7SL", Country = "UK" }
            };

            var customers = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe", Phone = "447445123456", Adress = addresses[0], Accounts = new List<Account> { new Account { Balance = 1000 } } },
                new Customer { FirstName = "Jane", LastName = "Smith", Phone = "447723234567", Adress = addresses[1], Accounts = new List<Account> { new Account { Balance = 2000 }, new Account { Balance = 3000 } } },
                new Customer { FirstName = "Bob", LastName = "Johnson", Phone = "14155550199", Adress = addresses[2], Accounts = new List<Account> { new Account { Balance = 4000 } } },
                new Customer { FirstName = "Alice", LastName = "Williams", Phone = "16475557890", Adress = addresses[3], Accounts = new List<Account> { new Account { Balance = 5000 } } },
                new Customer { FirstName = "Tom", LastName = "Brown", Phone = "447845345678", Adress = addresses[4], Accounts = new List<Account> { new Account { Balance = 6000 } } },
                new Customer { FirstName = "Mary", LastName = "Davis", Phone = "447912456789", Adress = addresses[0], Accounts = new List<Account> { new Account { Balance = 7000 }, new Account { Balance = 8000 } } },
                new Customer { FirstName = "Mark", LastName = "Garcia", Phone = "447780567890", Adress = addresses[1], Accounts = new List<Account> { new Account { Balance = 9000 } } },
                new Customer { FirstName = "Susan", LastName = "Rodriguez", Phone = "14155557890", Adress = addresses[2], Accounts = new List<Account> { new Account { Balance = 10000 } } },
                new Customer { FirstName = "Mike", LastName = "Lee", Phone = "16475557890", Adress = addresses[3], Accounts = new List<Account> { new Account { Balance = 11000 } } },
                new Customer { FirstName = "Linda", LastName = "Miller", Phone = "447546678901", Adress = addresses[4], Accounts = new List<Account> { new Account { Balance = 12000 }, new Account { Balance = 13000 }, new Account { Balance = 14000 } } }
            };

            context.Addresses.AddRange(addresses);
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
