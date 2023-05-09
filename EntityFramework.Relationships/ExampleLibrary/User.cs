namespace ExampleLibrary;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    // 1:1 relationship
    public UserProfile Profile { get; set; }

    // 1:N relationship
    public ICollection<Post> Posts { get; set; }

    // N:N relationship
    public ICollection<UserTag> UserTags { get; set; }
}