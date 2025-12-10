using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CourseWork.Models;

namespace CourseWork.Controllers;

[ApiExplorerSettings(IgnoreApi=true)]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Логін
    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        // Відображаємо сторінку логіну
        return View("/Views/User/Login.cshtml");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
            HttpContext.Session.SetString("Role", user.Role.Name);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Невірний email або пароль.";
        return View("~/Views/User/Login.cshtml");
    }


    // Реєстрація
    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        // Відображаємо сторінку реєстрації
        return View("/Views/User/Register.cshtml");
    }

    [HttpPost]
    [Route("register")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(User user)
    {
        // Призначаємо роль до валідації
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == 1); // Завантажуємо роль з БД

        if (role == null)
        {
            ModelState.AddModelError("RoleId", "Роль за замовчуванням не знайдена.");
        }
        else
        {
            user.RoleId = role.Id; // Призначаємо ID ролі
            user.Role = role;      // Призначаємо навігаційну властивість
        }

        // Ініціалізація порожніх колекцій
        user.Messages = new List<Message>();
        user.Enrollments = new List<Enrollment>();

        // Примусова очистка валідаційної помилки для Role
        ModelState.Remove("Role");

        // Перевірка валідації ModelState
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            Console.WriteLine(string.Join(", ", errors)); // Виводимо в консоль
            ViewBag.Errors = errors; // Передаємо помилки у View
            return View("/Views/User/Register.cshtml", user); // Повертаємо форму
        }

        // Якщо валідація пройшла успішно
        _context.Users.Add(user); // Додаємо користувача
        await _context.SaveChangesAsync(); // Зберігаємо в БД

        return RedirectToAction("Confirmation"); // Перенаправляємо на підтвердження
    }

    // Підтвердження реєстрації
    [HttpGet]
    [Route("confirmation")]
    public IActionResult Confirmation()
    {
        // Відображаємо сторінку підтвердження реєстрації
        return View("/Views/User/Confirmation.cshtml");
    }
    
    public IActionResult Details(int id)
    {
        var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View("~/Views/User/Details.cshtml", user);  // Вказуємо явний шлях
    }
    
    // Вихід із системи
    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        // Очищаємо сесію
        HttpContext.Session.Clear();

        // Перенаправляємо на логін
        return RedirectToAction("Login");
    }
}
