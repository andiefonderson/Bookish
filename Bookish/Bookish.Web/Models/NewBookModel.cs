namespace Bookish.Web.Models
{
    public class NewBookModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int NumberOfCopies { get; set; }
        public string ISBN { get; set; }

        //public NewBookModel(string title, string genre, int numberOfCopies, string iSBN)
        //{
        //    Title = title;
        //    Genre = genre;
        //    NumberOfCopies = numberOfCopies;
        //    ISBN = iSBN;
        //}
    }
}
