using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Enrollment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public Course Course { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime EnrollmentDate { get; set; }
}