namespace BookClub.Models
{
    public class ReadBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
