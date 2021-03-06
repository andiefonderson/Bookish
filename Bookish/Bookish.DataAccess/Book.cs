using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Bookish.DataAccess
{
    public class Book
    {
        // Create a class which represents a book and write a query which returns all of the books from your database and maps these to a list of books.
        // Make your console application call this function and print out the list of books to the console.

        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public string Genre { get; set; }
        public int NumberOfCopies { get; set; }
        public string ISBN { get; set; }

        public List<BookCopy> Copies()
        {
            return SqlReference.CopyListbyBookID(BookID);
        }

        public bool IsAvailable()
        {
            int copyUnavailable = 0;
            foreach (var copy in Copies())
            {
                if(copy.Available != 0)
                {
                    copyUnavailable++;
                }
            }

            return NumberOfCopies > copyUnavailable;
        }
    
    }
}
