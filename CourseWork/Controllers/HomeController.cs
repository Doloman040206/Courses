using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Controllers;

[ApiExplorerSettings(IgnoreApi=true)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Головна сторінка
    public IActionResult Index()
    {
        // Отримуємо дані користувача з сесії
        var userId = HttpContext.Session.GetString("UserId");
        var userName = HttpContext.Session.GetString("UserName");
        var userRole = HttpContext.Session.GetString("Role");

        // Передаємо значення в представлення
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        ViewBag.UserRole = userRole;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            // Зберігаємо ID, ім'я та роль в сесії
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
            HttpContext.Session.SetString("Role", user.Role.Name);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Невірний email або пароль.";
        return View("~/Views/User/Login.cshtml");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        // Очищаємо сесію
        HttpContext.Session.Clear();

        return RedirectToAction("Login", "Auth");
    }

    
    // Політика конфіденційності
    public IActionResult Privacy()
    {
        return View("~/Views/Home/Privacy.cshtml");
    }

    // Сторінка для адміністратора
    public IActionResult AdminDashboard()
    {
        return View("~/Views/Home/AdminDashboard.cshtml");
    }

    // Сторінка для студента
    public IActionResult StudentDashboard()
    {
        return View("~/Views/Home/StudentDashboard.cshtml");
    }

    // Сторінка для ментора
    public IActionResult MentorDashboard()
    {
        return View("~/Views/Home/MentorDashboard.cshtml");
    }


    // Обробка помилок
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
