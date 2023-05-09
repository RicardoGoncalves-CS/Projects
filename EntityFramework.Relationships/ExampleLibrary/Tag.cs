namespace ExampleLibrary;

public class Tag
{
    public int TagId { get; set; }
    public string TagName { get; set; }

    public ICollection<UserTag> UserTags { get; set; }
}
