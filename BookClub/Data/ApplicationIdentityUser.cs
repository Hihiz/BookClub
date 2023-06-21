using BookClub.Models;
using Microsoft.AspNetCore.Identity;

namespace BookClub.Data
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string Fio { get; set; }
    }
}
