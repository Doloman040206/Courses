# Online Courses — ASP.NET Core MVC

Цей проект є платформою онлайн курсів для студетів, який розроблено на ASP.NET Core MVC з використанням Entity Framework Core та MS SQL Server. Його основні можливості:
- Аутентифікація та реєстрація на курси
- Пошук і перегляд каталогу курсів
- Чітка система завдань та її перевірка

## Запуск локально

- Відкрий проект у Visual Studio.  
- Перевірити підключення до бази.
- Запустити проект через Start Debugging.

## Основні Файли

- Controllers/AssignmentController.cs — логіка відображення списку та деталей.

- Views/Assignment/Index.cshtml — список завдань.

- Views/Assignment/Details.cshtml — детальна інформація по одному завданню.

- Models/Assignment.cs — модель сутності завдання.

- Views/Shared/_Layout.cshtml — базовий шаблон сайту.
