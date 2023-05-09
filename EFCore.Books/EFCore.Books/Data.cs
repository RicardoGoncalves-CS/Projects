namespace EntityFramework;

internal static class Data
{
    public static IEnumerable<Author> Seed()
    {
        var authors = new List<Author>
        {
            new Author
            {
                Name = "Jane Austen", Books = new List<Book>
                {
                    new Book {Title = "Emma", PublicationYear = 1815},
                    new Book {Title = "Persuasion", PublicationYear = 1818},
                    new Book {Title = "Mansfield Park", PublicationYear = 1814},
                }
            },
            new Author
            {
                Name = "Ian Fleming", Books = new List<Book>
                {
                    new Book {Title = "Dr No", PublicationYear = 1958},
                    new Book {Title = "Goldfinger", PublicationYear = 1959},
                    new Book {Title = "From Russia with Love", PublicationYear = 1957}
                }
            }
        };

        return authors;
    }
}
