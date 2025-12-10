using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Assignments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.User)
                .OrderBy(a => a.DueDate)
                .ToListAsync();

            return View("~/Views/Assignments/Index.cshtml", assignments);
        }

        // GET: /Assignments/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var assignment = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assignment == null) return NotFound();

            return View("~/Views/Assignments/Details.cshtml", assignment);
        }

        // GET: /Assignments/MyAssignments
        [HttpGet]
        public async Task<IActionResult> MyAssignments()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login", "Auth");
            if (!int.TryParse(userIdString, out var userId)) return RedirectToAction("Login", "Auth");

            var my = await _context.Assignments
                .Where(a => a.UserId == userId)
                .Include(a => a.Course)
                .OrderBy(a => a.DueDate)
                .ToListAsync();

            return View("~/Views/Assignments/MyAssignments.cshtml", my);
        }
    }
}