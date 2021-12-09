using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class NewBookModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public string Genre { get; set; }
        public int NumberOfCopies { get; set; }
        public string ISBN { get; set; }

        public NewBookModel(string bookID, string title, int authorID, string genre, int numberOfCopies, string iSBN)
        {
            BookID = SqlReference.Library().Count;
            Title = title;
            AuthorID = SqlReference.Library().Count;
            Genre = genre;
            NumberOfCopies = numberOfCopies;
            ISBN = iSBN;
        }
    }
}
