/*Create database CourseWork*/

/*Use CourseWork*/

/*DROP TABLE [dbo].[Courses];
DROP TABLE [dbo].[Categories];
DROP TABLE [dbo].[Levels];*/



/*CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Levels (
	Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(100) NOT NULL,
	Description NVARCHAR(100) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
    LevelId INT FOREIGN KEY REFERENCES Levels(Id)
);*/

/*CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(100) NOT NULL,
	MiddleName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Password NVARCHAR(100) NOT NULL,
    RoleId INT FOREIGN KEY REFERENCES Roles(Id),
);*/

/*CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);*/

/*CREATE TABLE Enrollments (
    Id INT PRIMARY KEY IDENTITY(1,1),
	EnrollmentDate DateTime NOT NULL,
    CourseId INT FOREIGN KEY REFERENCES Courses(Id),
    UserId INT FOREIGN KEY REFERENCES Users(Id)
);*/

/*CREATE TABLE Assignments (
    Id INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(100) NOT NULL,
	Description NVARCHAR(100) NOT NULL,
	DueDate DateTime NOT NULL,
	IsRequired BIT NOT NULL,
	IsCompleted BIT NOT NULL,
	SubmissionUrl NVARCHAR(100) NOT NULL,
	SubmissionDate DateTime NOT NULL,
	Feedback NVARCHAR(100) NOT NULL,
	Grade INT NOT NULL,
	UserId INT FOREIGN KEY REFERENCES Users(Id),
    CourseId INT FOREIGN KEY REFERENCES Courses(Id)
);*/

/*INSERT INTO Categories (Name) 
VALUES ('Programming'),('Design'),('Marketing'),('Business'),('DevOps');*/

/*INSERT INTO Levels (Name) 
VALUES ('Beginner'),('Intermediate'),('Advanced');*/

/*INSERT INTO Courses (Title,Description, CategoryId, LevelId) 
VALUES 
('Основи C#','Вивчіть основи програмування на C# з нуля',1,1),
('Веб-розробка з ASP.NET Core','Створюйте сучасні веб-додатки з використанням ASP.NET Core',1,2),
('UI/UX Дизайн','Основи створення інтерфейсів користувача та досвіду використання',2,1),
('SEO та контент-маркетинг','Дізнайтеся, як оптимізувати сайти та створювати ефективний контент',3,2),
('Алгоритми та структури даних','Глибокий аналіз алгоритмів та ефективні структури даних',1,3);*/

/*INSERT INTO Roles (Name) 
VALUES ('Student'),('Mentor'),('Admin');*/

/*INSERT INTO Assignments (Title, Description, DueDate, IsRequired, IsCompleted, SubmissionUrl, SubmissionDate, Feedback, Grade, UserId, CourseId)
VALUES 
('Домашня робота з 1 з C#', 'Вивчити основи C#', '2026-12-01 23:59:00', 1, 0, 'https://example.com/submission/1', '2027-01-01 00:00:00', 'Ще не виконано', 11, 2, 1),
('Проект з ASP.NET Core', 'Зробити простий CRUD', '2026-12-10 23:59:00', 1, 1, 'https://example.com/submission/2', '2027-11-20 12:00:00', 'Добре зроблено', 10, 2, 2),
('Додаткове завдання з UI/UX Дизайну', 'Додаткові вправи', '2026-11-30 23:59:00', 0, 0, 'https://example.com/submission/3', '2027-01-01 00:00:00', 'Ще не виконано', 0, 2, 3);*/

/*Select*from Categories

Select*from Levels*/

/*Select*from Courses*/

/*Select*from Roles*/

/*Select*from Users*/

/*Select*from Enrollments*/

Select*from Assignments