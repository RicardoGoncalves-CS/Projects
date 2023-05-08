using EntityFramework;

var authors = Data.Seed();

foreach(var author in authors)
{
    Console.WriteLine($"{author} wrote:");

    foreach(var book in author.Books)
    {
        Console.WriteLine($"{book}");
    }

    Console.WriteLine();
}