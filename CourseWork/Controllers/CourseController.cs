using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork.Controllers;

[ApiExplorerSettings(IgnoreApi=true)]
public class CourseController : Controller
{
    private readonly ApplicationDbContext _context;

    // Конструктор контролера
    public CourseController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Перегляд доступних курсів
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Include(c => c.Category).Include(c => c.Level).ToListAsync();
        return View("~/Views/Course/Index.cshtml", courses);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Level)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return View("~/Views/Course/Details.cshtml", course);
    }


    // Запис на курс
    [HttpPost]
    public async Task<IActionResult> Enroll(int id)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            TempData["Error"] = "Для запису на курс необхідно увійти в систему.";
            return RedirectToAction("Login", "Auth");
        }

        var userIdInt = int.Parse(userId);

        // Перевірка існування курсу
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            TempData["Error"] = "Курс не знайдено.";
            return RedirectToAction("Index");
        }

        // Перевіряємо, чи вже записаний
        var enrollmentExists = await _context.Enrollments
            .AnyAsync(e => e.CourseId == id && e.UserId == userIdInt);

        if (enrollmentExists)
        {
            TempData["Error"] = "Ви вже записані на цей курс.";
            return RedirectToAction("Index");
        }

        // Додаємо запис у базу
        var enrollment = new Enrollment
        {
            CourseId = id,
            UserId = userIdInt,
            EnrollmentDate = DateTime.Now
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Ви успішно записались на курс!";
        return RedirectToAction("Details", "Course", new { id });
    }

    
    [HttpPost]
    public async Task<IActionResult> Unenroll(int id)
    {
        var userId = HttpContext.Session.GetString("UserId");
        var userIdInt = int.Parse(userId);

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.CourseId == id && e.UserId == userIdInt);

        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Profile", "User");
    }


}
