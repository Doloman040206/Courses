using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Профіль поточного користувача
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = await _context.Users
                .Include(u => u.Enrollments)
                    .ThenInclude(e => e.Course)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Явний шлях до view (можна замінити на View(user) якщо файл у стандартній папці)
            return View("~/Views/User/Profile.cshtml", user);
        }

        // Список користувачів
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            return View("~/Views/User/Index.cshtml", users);
        }

        // Деталі конкретного користувача
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();

            return View("~/Views/User/Details.cshtml", user);
        }

        // GET: створити нового користувача (форма)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");
            return View("~/Views/User/Create.cshtml");
        }

        // POST: створити нового користувача
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View("~/Views/User/Create.cshtml", user);
        }

        // GET: форма редагування
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View("~/Views/User/Edit.cshtml", user);
        }

        // POST: збереження редагування
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            // Якщо у моделі є навігаційні властивості, їх можна прибрати з ModelState
            ModelState.Remove("Role");
            ModelState.Remove("Password"); // якщо пароль не змінюється через цю форму

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
                return View("~/Views/User/Edit.cshtml", user);
            }

            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return NotFound();
            // Оновлюємо тільки дозволені поля
            existingUser.FirstName = user.FirstName;
            existingUser.MiddleName = user.MiddleName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            // не змінюємо Password/Role тут, якщо цього не потрібно

            try
            {
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        // GET: підтвердження видалення
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View("~/Views/User/Delete.cshtml", user);
        }

        // POST: видалити користувача
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Якщо це був поточний залогінений користувач — зробимо sign out і очистимо сесію
            if (User?.Identity?.Name == user.Email)
            {
                await HttpContext.SignOutAsync();
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}