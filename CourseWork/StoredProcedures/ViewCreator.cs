using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.StoredProcedures;

public class ViewCreator
{
    public static void CreateViews(ApplicationDbContext context)
    {
        var sql = @"
            CREATE VIEW CourseMaterials AS
            SELECT 
                c.Id AS CourseId, 
                c.Title AS CourseTitle, 
                c.Description AS CourseDescription, 
                m.Id AS MaterialId, 
                m.Title AS MaterialTitle, 
                m.ContentUrl AS MaterialUrl, 
                m.Type AS MaterialType
            FROM Courses c
            LEFT JOIN Materials m ON c.Id = m.CourseId;";
        context.Database.ExecuteSqlRaw(sql);
    }
}