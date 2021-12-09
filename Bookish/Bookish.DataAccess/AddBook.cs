using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class AddBook
    {
        public static string AddToBooks(string title, string genre, int numberOfCopies, string isbn)
        {
            Book newBook = new Book();

            newBook.BookID = SqlReference.Library().Count;
            newBook.Title = title;
            newBook.AuthorID = SqlReference.Library().Count;
            newBook.Genre = genre;
            newBook.NumberOfCopies = numberOfCopies;
            newBook.ISBN = isbn;

            return $"SELECT * FROM dbo.Books \n" +
                "INSERT INTO dbo.Books(BookID, Title, AuthorID, Genre, NumberOfCopies, ISBN) \n" +
                $"VALUES({newBook.BookID}, '{newBook.Title}', {newBook.AuthorID}, '{newBook.Genre}', {newBook.NumberOfCopies}, '{newBook.ISBN}')";
        }
    }
}
