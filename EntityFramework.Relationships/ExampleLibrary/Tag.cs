namespace ExampleLibrary;

public class Tag
{
    public int TagId { get; set; }
    public string TagName { get; set; }

    // N:N relationship with User
    public ICollection<UserTag> UserTags { get; set; }
}
