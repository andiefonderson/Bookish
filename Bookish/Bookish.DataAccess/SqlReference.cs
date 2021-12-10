using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class SqlReference
    {
        private static IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");

        // Create a class which represents a book and write a query which returns all of the books from your database and maps these to a list of books.
        // Make your console application call this function and print out the list of books to the console.

        public static List<Book> Library()
        {
            string queryString = "SELECT * FROM Books";
            var bookList = (List<Book>)db.Query<Book>(queryString);

            return bookList;
        }

        public static void AddToBooks(string title, string genre, int numberOfCopies, string isbn)
        {
            db.Execute(AddBook.AddToBooks(title, genre, numberOfCopies, isbn));
        }
    }
}
