using BookLibrary;

namespace EntityFramework;

internal class Data
{
    public static IEnumerable<Author> CreateData()
    {
        var authors = new List<Author>
        {
            new Author
            {
                Name = "Jane Austen", Books = new List<Book>
                {
                    new Book {Title = "Emma", YearOfPublication = 1815},
                    new Book {Title = "Persuasion", YearOfPublication = 1818},
                    new Book {Title = "Mansfield Park", YearOfPublication = 1814}
                }
            },
            new Author
            {
                Name = "Ian Fleming", Books = new List<Book>
                {
                    new Book {Title = "Dr No", YearOfPublication = 1958},
                    new Book {Title = "Goldfinger", YearOfPublication = 1959},
                    new Book {Title = "From Russia with Love", YearOfPublication = 1957}
                }
            }
        };

        return authors;
    }
}
