using System;
using System.Collections.Generic;
using Bookish.DataAccess;


namespace Bookish.Web.Models
{
    public class NewCopy
    {
        public NewCopy(int bookID, int available, DateTime dueDate)
        {
            BookID = bookID;
            Available = available;
            DueDate = dueDate;
        }

        public int BookID { get; set; }

        public int Available { get; set; }

        public DateTime DueDate { get; set; }
    }
}
