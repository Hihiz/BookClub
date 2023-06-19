using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class ReadBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [Display(Name = "Книги")]
        public Book? Book { get; set; }

        public int UserId { get; set; }
        [Display(Name = "Пользователь")]
        public User? User { get; set; }
    }
}
