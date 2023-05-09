namespace ExampleLibrary;

public class UserProfile
{
    public int UserProfileId { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }

    // 1:1 relationship with User
    public User User { get; set; }
}
