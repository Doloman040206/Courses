using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Assignment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Унікальний ідентифікатор завдання
    
    public int CourseId { get; set; } // Зовнішній ключ до курсу
    public Course Course { get; set; } // Навігаційна властивість до курсу

    public string Title { get; set; } // Назва завдання
    public string Description { get; set; } // Опис завдання
    public DateTime DueDate { get; set; } // Дата завершення завдання

    public bool IsRequired { get; set; } // Чи є завдання обов’язковим для виконання
    public bool IsCompleted { get; set; } // Статус виконання завдання

    public int UserId { get; set; } // Зовнішній ключ до користувача, що виконує завдання
    public User User { get; set; } // Навігаційна властивість до користувача

    public string SubmissionUrl { get; set; } // Посилання на завантажений файл (для подання)
    public DateTime? SubmissionDate { get; set; } // Дата подання завдання

    public string Feedback { get; set; } // Зворотній зв’язок від ментора
    public int? Grade { get; set; } // Оцінка за завдання (опціонально)
}