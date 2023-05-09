namespace ExampleLibrary;

public class UserTag
{
    // Join table to store the association between User and Tag with N:N relationship
    public int UserId { get; set; }
    public User User { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}