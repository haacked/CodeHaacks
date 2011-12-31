namespace MvcHaack.Ajax.Sample.Models
{
    public class ComicBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        public int IssueNumber { get; set; }
    }
}