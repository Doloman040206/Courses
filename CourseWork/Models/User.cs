using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [ForeignKey("Role")]
    public int RoleId { get; set; } // Зовнішній ключ

    // Навігаційна властивість
    public Role Role { get; set; }

    // Ініціалізуємо порожні колекції
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}