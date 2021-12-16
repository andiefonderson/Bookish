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
        // Create a class which represents a book and write a query which returns all of the books from your database and maps these to a list of books.
        // Make your console application call this function and print out the list of books to the console.

        private static IDbConnection DBConnection()
        {
            return new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
        }

        private static string SelectDatabase(string database)
        {
            return $"SELECT * FROM {database}";
        }

        private static string SelectDatabase(string database, string column, string value)
        {
            return $"SELECT * FROM {database} WHERE {column} = {value}";
        }

        private static string AddToDatabase(string authorName)
        {
            int authorID = AuthorList().Count + 1;

            string queryString = SelectDatabase("Authors") + "\n" +
                $"INSERT INTO Authors(AuthorID, AuthorName) \n" +
                $"VALUES({authorID}, '{authorName}')";

            return queryString;
        }

        private static string AddToDatabase(string bookTitle, int authorID, string genre, int numberOfCopies, string isbn)
        {
            int bookID = Library().Count + 1;

            string queryString = SelectDatabase("Books") + "\n" +
                $"INSERT INTO Books(BookID, Title, AuthorID, Genre, NumberOfCopies, ISBN) \n" +
                $"VALUES({bookID}, '{bookTitle}', {authorID}, '{genre}', {numberOfCopies}, '{isbn}')";

            return queryString;
        }

        private static string AddToDatabase(int bookID, bool copyAvailable)
        {
            int copyID = CopiesList().Count + 1;
            int available = copyAvailable ? 1 : 0;

            string queryString = SelectDatabase("Copies") + "\n" +
                $"INSERT INTO Copies(CopyID, BookID, Available) \n" +
                $"VALUES({copyID}, {bookID}, {available})";

            return queryString;
        }

        public static List<Author> AuthorList()
        {
            var db = DBConnection();
            var authorList = (List<Author>)db.Query<Author>(SelectDatabase("Authors"));
            db.Close();

            return authorList;
        }

        public static List<BookCopy> CopiesList()
        {
            var db = DBConnection();
            var copiesList = (List<BookCopy>)db.Query<BookCopy>(SelectDatabase("Copies"));
            db.Close();

            return copiesList;
        }

        public static List<BookCopy> CopyListbyBookID(int bookID)
        {
            var db = DBConnection();
            var copiesList = (List<BookCopy>)db.Query<BookCopy>(SelectDatabase("Copies", "BookID", bookID.ToString()));
            db.Close();

            return copiesList;
        }

        public static List<Book> Library()
        {
            var db = DBConnection();
            var bookList = (List<Book>)db.Query<Book>(SelectDatabase("Books"));
            db.Close();

            foreach (Book book in bookList)
            {
                List<BookCopy> bookCopies = CopyListbyBookID(book.BookID);

                if (bookCopies.Count < book.NumberOfCopies)
                {
                    for (int i = 0; i < book.NumberOfCopies; i++)
                    {
                        AddToCopies(book.BookID, true);
                    }
                }
            }

            return bookList;
        }

        public static Book GetBook(int bookID)
        {
            var db = DBConnection();
            string queryString = SelectDatabase("Books", "BookID", bookID.ToString());
            Book book = (Book)db.Query<Book>(queryString).Single();
            db.Close();

            return book;
        }

        public static List<Book> Search(string column, string value)
        {
            string queryString = $"SELECT * FROM Books WHERE {column} = '{value}'";
            var db = DBConnection();
            var bookList = (List<Book>)db.Query<Book>(queryString);
            return bookList;
        }

        public static void AddToBooks(string title, string genre, int numberOfCopies, string isbn, string authorID)
        {
            var db = DBConnection();
            db.Execute(AddToDatabase(title, int.Parse(authorID), genre, numberOfCopies, isbn));
            db.Close();
        }

        public static void AddToCopies(int bookID, bool copyAvailable)
        {
            var db = DBConnection();
            db.Execute(AddToDatabase(bookID, copyAvailable));
            db.Close();
        }

        public static void UpdateCopy (int copyID, bool available, string borrowerEmail, DateTime dueDate)
        {
            int availableBit = available ? 1 : 0; 
            var db = DBConnection();
            string queryString = "UPDATE Copies" +
                $"\n SET Available = {availableBit}, BorrowerEmail = '{borrowerEmail}', DueDate = '{dueDate.Date.ToString("yyyy-MM-dd")}'" +
                $"\n WHERE CopyID = {copyID};";
            db.Execute(queryString);
            db.Close();
        }
    }
}
