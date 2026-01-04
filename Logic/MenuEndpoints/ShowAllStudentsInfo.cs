using BankApp.HelperClasses;
using ErikLabb4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

class ShowAllStudentsInfo
{
    internal static void Entry(SchoolContext dbContext)
    {
        while (true)
        {
            Console.WriteLine("Press 0 to display all students info\nPlease enter the Id of the class you want to display data from");
            Console.WriteLine("NOTE: Writing all data is from 120 different students");
            Console.WriteLine("\n Availible classes: ");
            dbContext.Classes
                .ToList()
                .ForEach(Class => Console.Write(Class.ClassId + ", "));
            Console.WriteLine();
            try
            {
                int choice = GetUserInput.ValidateInt("Select an option");

                if (choice == 0)
                {
                    DisplayAllStudentsInfo(dbContext);
                }
                else
                {
                    Class chosenClass = dbContext.Classes
                    .Include(c => c.Students)
                    .ThenInclude(s => s.StudentCourseRecords)
                    .ThenInclude(s => s.Course)
                    .Single(c => c.ClassId == choice);

                    DisplayStudentInfoByClass(chosenClass);
                }
                return;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    Console.WriteLine("no class matches that id, try again\n");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    private static void DisplayStudentInfoByClass(Class chosenClass)
    {
        foreach (var student in chosenClass.Students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
            Console.WriteLine($"class ID:{student.Class.ClassId}");
            Console.WriteLine("Current Courses: \n");

            student.StudentCourseRecords
                .ToList()
                .ForEach(student =>
                {
                    Console.WriteLine($"Course name: {student.Course.CourseName}: GRADE: {student.StudentFinalGrade} \n");

                });



            var gradeMapping = new Dictionary<string, int>
                    {
                        { "E", 60 },
                        { "D", 70 },
                        { "C", 80 },
                        { "B", 90 },
                        { "A", 100},
                        {"F", 0}
                    };

            List<int> grades = student.StudentCourseRecords
                .Select(record => gradeMapping[record.StudentFinalGrade ?? "F"])
                .ToList();



            Console.WriteLine("\nAverage Grade: " + (int)grades.Average() + "\n");
            Console.WriteLine("-------------------------------\n");
        }
    }
    private static void DisplayAllStudentsInfo(SchoolContext dbContext)
    {
        var allStudents = dbContext.Students
                        .Include(s => s.StudentCourseRecords)
                        .ThenInclude(s => s.Course);


        foreach (var student in allStudents)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName}");
            Console.WriteLine($"class ID:{student.Class.ClassId}");
            Console.WriteLine("Current Courses: \n");

            student.StudentCourseRecords
                .ToList()
                .ForEach(student =>
                {
                    Console.WriteLine($"Course name: {student.Course.CourseName}: GRADE: {student.StudentFinalGrade} \n");
                    
                });



            var gradeMapping = new Dictionary<string, int>
                    {
                        { "E", 60 },
                        { "D", 70 },
                        { "C", 80 },
                        { "B", 90 },
                        { "A", 100},
                        {"F", 0}
                    };

            List<int> grades = student.StudentCourseRecords
                .Select(record => gradeMapping[record.StudentFinalGrade ?? "F"])
                .ToList();



            Console.WriteLine("\nAverage Grade: " + (int)grades.Average() + "\n");
            Console.WriteLine("-------------------------------\n");
        }
    }
}