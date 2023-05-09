namespace ExampleLibrary;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    // 1:1 relationship with UserProfile
    public UserProfile Profile { get; set; }

    // 1:N relationship with Post
    public ICollection<Post> Posts { get; set; }

    // N:N relationship with Tag
    public ICollection<UserTag> UserTags { get; set; }
}