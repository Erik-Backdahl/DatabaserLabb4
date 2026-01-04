using ErikLabb4.Models;
using Microsoft.EntityFrameworkCore;

class ModifyCourses
{
    internal  static void ShowCourseActivity(SchoolContext dbContext)
    {
        var allCourses = dbContext.Courses.Include(c => c.StudentCourseRecords);

        List<Course> activeCourses = [];
        List<Course> inactiveCourses = [];

        
        foreach(Course course in allCourses)
        {
            if(course.StudentCourseRecords.Count > 0)
            {
                activeCourses.Add(course);
            }
            else
            {
                inactiveCourses.Add(course);
            }
        }

        Console.WriteLine("Active Courses:\n");
        foreach(Course course in activeCourses)
        {
            Console.WriteLine($"{course.CourseName}");
        }

        Console.WriteLine("\n---------------\n");

        Console.WriteLine("Inactive Courses:");
        foreach(Course course in inactiveCourses)
        {
            Console.WriteLine($"{course.CourseName}");
        }
    }
}