using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int CategoryId { get; set; } // Зовнішній ключ
    public Category Category { get; set; } // Навігаційна властивість

    public int LevelId { get; set; } // Зовнішній ключ
    public Level Level { get; set; } // Навігаційна властивість

    public List<Material> Materials { get; set; }
    public List<Assignment> Assignments { get; set; }
    public List<Enrollment> Enrollments { get; set; }
}