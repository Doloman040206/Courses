using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.StoredProcedures;

public class StoredProcedures
{
    // Процедура 1: Пошук студента за email
    public static void CreateFindStudentByEmail(ApplicationDbContext context)
    {
        var sql = @"
            CREATE PROCEDURE FindStudentByEmail
                @Email NVARCHAR(100)
            AS
            BEGIN
                SELECT 
                    u.Id AS UserId, 
                    u.FirstName, 
                    u.LastName, 
                    u.Email, 
                    r.Name AS RoleName,
                    c.Title AS EnrolledCourse
                FROM Users u
                JOIN Roles r ON u.RoleId = r.Id
                LEFT JOIN Enrollments e ON u.Id = e.UserId
                LEFT JOIN Courses c ON e.CourseId = c.Id
                WHERE u.Email = @Email;
            END";
        context.Database.ExecuteSqlRaw(sql);
    }

    // Процедура 2: Оцінки студента
    public static void CreateGetStudentGrades(ApplicationDbContext context)
    {
        var sql = @"
            CREATE PROCEDURE GetStudentGrades
                @UserId INT
            AS
            BEGIN
                SELECT 
                    u.Id AS UserId, 
                    u.FirstName, 
                    u.LastName, 
                    a.Title AS AssignmentTitle, 
                    a.Grade
                FROM Users u
                JOIN Assignments a ON u.Id = a.UserId
                WHERE u.Id = @UserId AND a.Grade IS NOT NULL;
            END";
        context.Database.ExecuteSqlRaw(sql);
    }
}