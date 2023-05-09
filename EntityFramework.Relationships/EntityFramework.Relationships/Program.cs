using EntityFramework.Relationships;
using ExampleLibrary;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<RelationshipsContext>()
    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EntityFramework.Relationships;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
    .Options;

using var db = new RelationshipsContext(options);

db.Database.EnsureCreated();

if (db.Users.Any())
{
    db.Users.RemoveRange(db.Users);

    if(db.UserProfiles.Any())
        db.UserProfiles.RemoveRange(db.UserProfiles);

    if (db.Posts.Any())
        db.Posts.RemoveRange(db.Posts);
}

// >> To create and save dummy data
var users = SeedData.Create();
db.Users.AddRange(users);
db.SaveChanges();

foreach (var user in db.Users.Include(u => u.Profile))
{
    Console.WriteLine($"{user.UserName} is {user.Profile.Age} and the full name is {user.Profile.FullName}");
    
    if (user.Posts != null)
    {
        Console.WriteLine("Posts:");
        
        foreach (var post in user.Posts)
        {
            Console.WriteLine(post.Title);
        }
    }

    Console.WriteLine();
}