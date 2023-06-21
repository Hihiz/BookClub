using BookClub.Data;
using BookClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationIdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["UserId"] = _userManager.GetUserId(User);

            return View(_db.Books.ToList());
        }

        public async Task<IActionResult> AddListBook(int id)
        {
            var userId = _userManager.GetUserId(User);

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
                    UserId = userId
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

        public async Task<IActionResult> CheckList()
        {
            var userId = _userManager.GetUserId(User);

            var applicationContext = _db.ReadBooks.Include(r => r.Book).Include(r => r.User).Where(r => r.UserId == userId);

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
    }
}