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
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring 1:1 relationship between Users and UserProfiles
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserProfileId)
            .IsRequired();

        // Configuring 1:N relationship between Users and Posts
        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        // Configuring N:N relationship between Users and Tags
        modelBuilder.Entity<UserTag>()
            .HasKey(ut => new { ut.UserId, ut.TagId });

        modelBuilder.Entity<UserTag>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTags)
            .HasForeignKey(ut => ut.UserId);

        modelBuilder.Entity<UserTag>()
            .HasOne(ut => ut.Tag)
            .WithMany(t => t.UserTags)
            .HasForeignKey(ut => ut.TagId);
    }
}
