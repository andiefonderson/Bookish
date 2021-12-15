using Bookish.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bookish.Web.Models
{
    public class NewBookModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int NumberOfCopies { get; set; }
        public string ISBN { get; set; }
        public string AuthorID { get; set; }
        public List<SelectListItem> AuthorNameList { get; set; }
    }
}
