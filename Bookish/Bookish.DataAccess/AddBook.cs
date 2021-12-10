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
            int BookID = SqlReference.Library().Count + 1;
            int AuthorID = 1;

            return $"SELECT * FROM dbo.Books \n" +
                "INSERT INTO dbo.Books(BookID, Title, AuthorID, Genre, NumberOfCopies, ISBN) \n" +
                $"VALUES({BookID}, '{title}', {AuthorID}, '{genre}', {numberOfCopies}, '{isbn}')";
        }
    }
}
