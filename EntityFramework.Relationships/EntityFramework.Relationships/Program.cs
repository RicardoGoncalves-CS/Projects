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
    db.UserProfiles.RemoveRange(db.UserProfiles);
    db.Posts.RemoveRange(db.Posts);
    db.Tags.RemoveRange(db.Tags);
}

// >> To create and save dummy data
var users = SeedData.Create();
db.Users.AddRange(users);
db.SaveChanges();

foreach (var user in db.Users
    .Include(u => u.Profile)
    .Include(u => u.Posts)
    .Include(u => u.UserTags)
    .ThenInclude(ut => ut.Tag))
{
    Console.WriteLine($"Username: {user.UserName}");
    Console.WriteLine($"Full name: {user.Profile.FullName}");
    Console.WriteLine($"Age: {user.Profile.Age}");
    
    if (user.Posts.Count > 0)
    {
        Console.WriteLine("Posts:");
        
        foreach (var post in user.Posts)
        {
            Console.WriteLine(post.Title);
        }
    }

    if(user.UserTags.Count > 0)
    {
        Console.WriteLine("Tags:");

        foreach(var userTag in user.UserTags)
        {
            Console.WriteLine(userTag.Tag.TagName);
        }
    }
    
    Console.WriteLine();
}