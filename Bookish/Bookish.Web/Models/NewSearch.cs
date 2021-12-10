using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookish.Web.Models
{
    public class NewSearch
    {
        public string Column { get; set; }
        public string Value { get; set; }

        public List<Bookish.DataAccess.Book> BookList { get; set; }
    }

    public enum Columns
    {
        Title, 
        AuthorID,
        Genre
    }
}
