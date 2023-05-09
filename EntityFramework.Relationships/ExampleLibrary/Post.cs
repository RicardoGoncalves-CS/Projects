namespace ExampleLibrary;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }

    // 1:N relationship with User
    public int UserId { get; set; }
    public User User { get; set; }
}
