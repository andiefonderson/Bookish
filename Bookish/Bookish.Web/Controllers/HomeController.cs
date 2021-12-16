using Bookish.DataAccess;
using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Book> books = new List<Book>();

            if (User.Identity.IsAuthenticated)
            {                
                foreach (var copy in SqlReference.CopiesList())
                {
                    if(copy.BorrowerEmail == User.Identity.Name)
                    {
                        books.Add(SqlReference.GetBook(copy.BookID));
                    }
                }
            }

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Library()
        {
            if(TempData["successMessage"] != null)
            {
                if (TempData["successMessage"].ToString() == "success")
                {
                    ViewBag.SuccessMessage = "success";
                    TempData["successMessage"] = "";
                }
                else
                {
                    ViewBag.SuccessMessage = "";
                }
            }
            
            List<Book> bookList = SqlReference.Library();
            List<Book> sortedList = bookList.OrderBy(o => o.Title).ToList();
            return View(sortedList);
        }

        public IActionResult ViewBook(int bookID)
        {
            Book book = SqlReference.GetBook(bookID);
            return View(book);
        }

        [HttpPost]
        public IActionResult CheckOutBook(int bookID)
        {
            Book book = SqlReference.GetBook(bookID);
            foreach (var copy in book.Copies())
            {
                if(copy.BorrowerEmail == null)
                {
                    string borrowerEmail = User.Identity.Name;
                    DateTime dueDate = DateTime.Now.AddDays(14);
                    SqlReference.UpdateCopy(copy.CopyID, false, borrowerEmail, dueDate);
                    
                    break;
                }
            }

            return RedirectToAction("CheckOut", new {bookID=bookID});
        }



        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(NewSearch newSearch)
        {
            try
            {
                List<Book> bookList = SqlReference.Search(newSearch.Column, newSearch.Value);
                newSearch.BookList = bookList;

                return View(newSearch);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult AddBook()
        {
            NewBookModel newBook = new NewBookModel();
            newBook.AuthorNameList = CreateAuthorList();
            return View(newBook);
        }

        private static List<SelectListItem> CreateAuthorList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var author in SqlReference.AuthorList())
            {
                items.Add(new SelectListItem { Text = author.AuthorName, Value = author.AuthorID.ToString() });
            }
            return items;
        }

        [HttpPost]
        public IActionResult AddBook(NewBookModel newBook)
        {
            newBook.AuthorNameList = CreateAuthorList();
            var selectedItem = newBook.AuthorNameList.Find(p => p.Value == newBook.AuthorID.ToString());
            if(selectedItem != null)
            {
                selectedItem.Selected = true;
                ViewBag.Message = "Author: " + selectedItem.Text;
                ViewBag.Message += "\nAuthorID: " + newBook.AuthorID;
            }

            try
            {
                SqlReference.AddToBooks(newBook.Title, newBook.Genre, newBook.NumberOfCopies, newBook.ISBN, newBook.AuthorID);
                TempData["successMessage"] = "success";
                return RedirectToAction("Library");
            }
            catch (Exception)
            {
                ViewBag.Error = "error";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CheckOut(int bookID)
        {
            Book newBook = SqlReference.GetBook(bookID);
            return View(newBook.Copies());
        }

        [HttpPost]
        public IActionResult CheckOut()
        {
            // Book newBook = SqlReference.GetBook(bookID);
            //if (newBook.NumberOfCopies >= newBook.Borrowers.Count)
            //{
            //    NewCopy newCopy = new NewCopy(bookID, 1, DateTime.Now.AddDays(14));
            //    newBook.Borrowers.Append(User.Identity.Name);
            //}
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
