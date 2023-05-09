using ExampleLibrary;

namespace EntityFramework.Relationships
{
    internal class SeedData
    {
        public static IEnumerable<User> Create()
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "James", Profile = new UserProfile
                    {
                        FullName = "James Taylor",
                        Age = 27
                    }
                },
                new User
                {
                    UserName = "Emma", Profile = new UserProfile
                    {
                        FullName = "Emma Watson",
                        Age = 41
                    }

                },
                new User
                {
                    UserName = "Peter", Profile = new UserProfile
                    {
                        FullName = "Peter Smith",
                        Age = 35
                    }
                }
            };

            return users;
        }
    }
}
