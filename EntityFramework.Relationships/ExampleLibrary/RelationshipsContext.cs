using Microsoft.EntityFrameworkCore;

namespace ExampleLibrary;

// >> ONE-TO-ONE relationship
//
// >> User and UserProfile hava a 1:1 relationship
// >> This is because a User has only one UserProfile and a UserProfile has only one User
// >> User has a UserProfile property and a UserProfile has a User property
// >> The Primary Key of User is used has the Foreign Key for UserProfile

public class RelationshipsContext : DbContext
{
    public RelationshipsContext(DbContextOptions<RelationshipsContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserProfileId)
            .IsRequired();
    }
}
