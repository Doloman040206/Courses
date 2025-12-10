using Microsoft.EntityFrameworkCore;
namespace CourseWork.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; } // Додано категорії
    public DbSet<Level> Levels { get; set; }       // Додано рівні
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=CourseWork;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Зв'язок User і Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Зв'язок Course і Category
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Category)
            .WithMany(cat => cat.Courses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Зв'язок Course і Level
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Level)
            .WithMany(l => l.Courses)
            .HasForeignKey(c => c.LevelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Зв'язок Message
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Material>()
            .Property(m => m.Type)
            .HasConversion<int>();

        modelBuilder.Entity<Message>()
            .Property(m => m.Type)
            .HasConversion<int>();
        
        // Додавання ролей
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Student" },
            new Role { Id = 2, Name = "Mentor" },
            new Role { Id = 3, Name = "Admin" }
        );
        
        // Додавання категорій
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Programming" },
            new Category { Id = 2, Name = "Design" },
            new Category { Id = 3, Name = "Marketing" },
            new Category { Id = 4, Name = "Business" },
            new Category { Id = 5, Name = "DevOps" }
        );

        // Додавання рівнів
        modelBuilder.Entity<Level>().HasData(
            new Level { Id = 1, Name = "Beginner" },
            new Level { Id = 2, Name = "Intermediate" },
            new Level { Id = 3, Name = "Advanced" }
        );
        
        // Додавання курсів
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Основи C#",
                Description = "Вивчіть основи програмування на C# з нуля.",
                CategoryId = 1,
                LevelId = 1
            },
            new Course
            {
                Id = 2,
                Title = "Веб-розробка з ASP.NET Core",
                Description = "Створюйте сучасні веб-додатки з використанням ASP.NET Core.",
                CategoryId = 1,
                LevelId = 2
            },
            new Course
            {
                Id = 3,
                Title = "UI/UX Дизайн",
                Description = "Основи створення інтерфейсів користувача та досвіду використання.",
                CategoryId = 2,
                LevelId = 1
            },
            new Course
            {
                Id = 4,
                Title = "SEO та контент-маркетинг",
                Description = "Дізнайтеся, як оптимізувати сайти та створювати ефективний контент.",
                CategoryId = 3,
                LevelId = 2
            },
            new Course
            {
                Id = 5,
                Title = "Алгоритми та структури даних",
                Description = "Глибокий аналіз алгоритмів та ефективні структури даних.",
                CategoryId = 1,
                LevelId = 3
            });
        base.OnModelCreating(modelBuilder);
    }
}