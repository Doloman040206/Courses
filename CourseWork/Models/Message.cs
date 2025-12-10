using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models;

public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Унікальний ідентифікатор повідомлення

    public int SenderId { get; set; } // Зовнішній ключ для відправника
    public User Sender { get; set; } // Навігаційна властивість для відправника

    public int ReceiverId { get; set; } // Зовнішній ключ для отримувача
    public User Receiver { get; set; } // Навігаційна властивість для отримувача

    public string Content { get; set; } // Текст повідомлення
    public DateTime SentDate { get; set; } // Дата і час відправлення

    public bool IsRead { get; set; } // Статус прочитання (true – прочитано, false – не прочитано)

    public MessageType Type { get; set; } // Тип повідомлення (текстове, системне, файл тощо)
}