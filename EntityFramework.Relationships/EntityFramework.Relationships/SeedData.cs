using ExampleLibrary;

namespace EntityFramework.Relationships;

// >> SeedData is used to generate dummy data to populate the database
internal class SeedData
{
    public static IEnumerable<User> Create()
    {
        var users = new List<User>
        {
            new User
            {
                UserName = "James", 
                Profile = new UserProfile
                {
                    FullName = "James Taylor",
                    Age = 27
                },
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Object-Oriented Programming using C#"
                    },
                    new Post
                    {
                        Title = "Model creation in code first approach using EntityFramework"
                    }
                },
                UserTags = new List<UserTag>
                {
                    new UserTag
                    {
                        Tag = new Tag { TagName = "Programming" }
                    },
                    new UserTag
                    {
                        Tag = new Tag { TagName = "C#" }
                    }
                }
            },
            new User
            {
                UserName = "Emma", 
                Profile = new UserProfile
                {
                    FullName = "Emma Watson",
                    Age = 41
                },
                UserTags = new List<UserTag>
                {
                    new UserTag
                    {
                        Tag = new Tag { TagName = "Acting" }
                    },
                    new UserTag
                    {
                        Tag = new Tag { TagName = "Films" }
                    }
                }

            },
            new User
            {
                UserName = "Peter", 
                Profile = new UserProfile
                {
                    FullName = "Peter Smith",
                    Age = 35
                },
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Uses of EntityFramework Core"
                    }
                },
                UserTags = new List<UserTag>
                {
                    new UserTag
                    {
                        Tag = new Tag { TagName = ".NET" }
                    },
                    new UserTag
                    {
                        Tag = new Tag { TagName = "Databases" }
                    },
                    new UserTag
                    {
                        Tag = new Tag { TagName = "Microsoft" }
                    }
                }
            }
        };

        return users;
    }
}
