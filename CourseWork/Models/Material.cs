using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Material
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Унікальний ідентифікатор матеріалу

    public int CourseId { get; set; } // Зовнішній ключ до курсу
    public Course Course { get; set; } // Навігаційна властивість для зв'язку з курсом

    public string Title { get; set; } // Назва матеріалу
    public string ContentUrl { get; set; } // Посилання на ресурс (відео, PDF, текст)

    public MaterialType Type { get; set; } // Тип матеріалу (відео, текст, тест тощо)

    public DateTime UploadDate { get; set; } // Дата завантаження
    public bool IsActive { get; set; } // Статус активності матеріалу (доступний чи ні)
}