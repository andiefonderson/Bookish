using System;

namespace Bookish.DataAccess
{
    public class BookCopy
    {
        public int CopyID { get; set; }
        public int BookID { get; set; }
        public int Available { get; set; }
        public DateTime DueDate { get; set; }
        public int BorrowerID { get; set; }
    }
}
