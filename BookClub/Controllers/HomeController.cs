using BookClub.Data;
using BookClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookClub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index() => View(_db.Books.ToList());

        public async Task<IActionResult> AddListBook(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Book book = await _db.Books.FindAsync(id);

            if (ModelState.IsValid)
            {
                ReadBook readBook = new ReadBook()
                {
                    BookId = book.Id,
                    UserId = 1
                };

                try
                {
                    _db.ReadBooks.Add(readBook);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CheckList(int id = 1)
        {
            var applicationContext = _db.ReadBooks.Include(r => r.Book).Include(r => r.User).Where(r => r.UserId == id);

            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> DeleteListBook(int id)
        {
            ReadBook readBook = await _db.ReadBooks.FindAsync(id);

            if (readBook != null)
            {
                _db.ReadBooks.Remove(readBook);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}