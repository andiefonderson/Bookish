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

        private static string AddToDatabase(string userFirstName, string userSurname, string userEmail, string userPassword)
        {
            int userID = UsersList().Count + 1;

            string queryString = SelectDatabase("Users") + "\n" +
                $"INSERT INTO Users(UserID, FirstName, Surname, Email, Password) \n" +
                $"VALUES({userID}, '{userFirstName}', '{userSurname}', '{userEmail}', '{userPassword}')";

            return queryString;
        }

        private static string AddToDatabase(int bookID, bool copyAvailable, DateTime dueDate, int borrowerID)
        {
            int copyID = CopiesList().Count + 1;
            int available = copyAvailable ? 1 : 0;

            string queryString = SelectDatabase("Copies") + "\n" +
                $"INSERT INTO Copies(CopyID, BookID, Available, DueDate, BorrowerID) \n" +
                $"VALUES({copyID}, {bookID}, {available}, {dueDate:dd-mm-yyyy}, {borrowerID})";

            return queryString;
        }


        public static List<Author> AuthorList()
        {
            var db = DBConnection();
            var authorList = (List<Author>)db.Query<Author>(SelectDatabase("Authors"));
            db.Close();

            return authorList;
        }

        public static List<Book> Library()
        {
            var db = DBConnection();
            var bookList = (List<Book>)db.Query<Book>(SelectDatabase("Books"));
            db.Close();

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

        public static List<User> UsersList()
        {
            var db = DBConnection();
            var userList = (List<User>)db.Query<User>(SelectDatabase("Users"));
            db.Close();

            return userList;
        }

        public static List<BookCopy> CopiesList()
        {
            var db = DBConnection();
            var copiesList = (List<BookCopy>)db.Query<BookCopy>(SelectDatabase("Copies"));
            db.Close();

            return copiesList;
        }

        public static void AddToBooks(string title, string genre, int numberOfCopies, string isbn, string authorID)
        {
            var db = DBConnection();
            db.Execute(AddToDatabase(title, int.Parse(authorID), genre, numberOfCopies, isbn));
            db.Close();
        }
    }
}
