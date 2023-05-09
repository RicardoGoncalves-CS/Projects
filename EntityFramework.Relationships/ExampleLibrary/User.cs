namespace ExampleLibrary;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    // :1 relationship
    public UserProfile Profile { get; set; }

    // :N relationship
    public ICollection<Post> Posts { get; set; }
}