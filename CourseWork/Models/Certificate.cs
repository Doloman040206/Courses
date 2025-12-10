using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Certificate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Унікальний ідентифікатор сертифіката

    public int UserId { get; set; } // Зовнішній ключ до користувача
    public User User { get; set; } // Навігаційна властивість до користувача

    public int CourseId { get; set; } // Зовнішній ключ до курсу
    public Course Course { get; set; } // Навігаційна властивість до курсу

    public DateTime IssuedDate { get; set; } // Дата видачі сертифіката
    public string CertificateUrl { get; set; } // URL для завантаження сертифіката

    public bool IsValid { get; set; } // Статус дійсності сертифіката
}