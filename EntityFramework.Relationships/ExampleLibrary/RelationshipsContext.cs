using Microsoft.EntityFrameworkCore;

namespace ExampleLibrary;

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
        // >> ONE-TO-ONE relationship
        //
        // >> User and UserProfile hava a 1:1 relationship
        // >> This is because a User has only one UserProfile and a UserProfile has only one User
        // >> User has a UserProfile property and a UserProfile has a User property
        // >> The Primary Key of User is used has the Foreign Key for a UserProfile and vice-versa

        // Configuring 1:1 relationship between Users and UserProfiles
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserProfileId)
            .IsRequired();

        // >> ONE-TO-MANY relationship
        //
        // >> User and Post hava a 1:N relationship
        // >> This is because a User has many Posts but a Post has only one User
        // >> User has a collection of Posts property and a Post has a User and a UserId property
        // >> The Primary Key of User is used has the Foreign Key for a Post and vice-versa

        // Configuring 1:N relationship between Users and Posts
        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        // >> MANY-TO-MANY relationship
        //
        // >> User and Tag hava a N:N relationship
        // >> This is because a User can have many Tags and a Tag can have many Users
        // >> User has a collection of Tags property and a Tag has a collection of Users property
        // >> UserTag is used as a Join Table holding the relations between User and Tag

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
