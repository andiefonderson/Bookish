using System;
using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Book> bookList = SqlReference.Library();

            //foreach (Book item in bookList)
            //{
            //    Console.WriteLine(item.Title.ToString());
            //}

            Book newBook = SqlReference.GetBook(1);
            Console.WriteLine(newBook.Title);
            Console.ReadLine();
        }
    }
}
