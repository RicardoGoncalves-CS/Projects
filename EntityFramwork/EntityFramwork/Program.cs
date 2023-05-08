using BookLibrary;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<BooksContext>()
    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EntityFramework.Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
    .Options;

using var db = new BooksContext(options);

db.Database.EnsureCreated();

// >> Created dummy data and saved to the database
//var authors = Data.CreateData();

//db.Authors.AddRange(authors);
//db.SaveChanges();

var recentBooks = db.Books.Where(b => b.YearOfPublication > 1900);

foreach (var book in recentBooks.Include(b => b.Author))
{
    Console.WriteLine($"{book} was written by {book.Author}");
}

// >> A different query
//foreach (var author in db.Authors.Include(a => a.Books))
//{
//    Console.WriteLine($"{author} wrote:");

//    foreach (var book in author.Books)
//    {
//        Console.WriteLine(book);
//    }

//    Console.WriteLine();
//}