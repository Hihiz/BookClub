using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "Книги")]
        public string Title { get; set; }
    }
}
