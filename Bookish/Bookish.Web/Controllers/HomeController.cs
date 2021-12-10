﻿using Bookish.DataAccess;
using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Library()
        { 
            List<Book> bookList = SqlReference.Library();
            List<Book> sortedList = bookList.OrderBy(o => o.Title).ToList();
            return View(sortedList);
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
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(NewBookModel newBook)
        {
            try
            {
                SqlReference.AddToBooks(newBook.Title, newBook.Genre, newBook.NumberOfCopies, newBook.ISBN);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
