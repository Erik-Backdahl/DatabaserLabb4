using BankApp.HelperClasses;
using ErikLabb4.Models;

class ModifyStudents
{
    internal static void GetAllStudents(SchoolContext dbContext)
    {
        var students = dbContext.Students
        .Select(s => s.FirstName)
        .ToList();

        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
    internal static void GetAllStudentsFromClass(SchoolContext dbContext)
    {
        int requestedClass = GetUserInput.ValidateInt("Enter class ID: ");

        var students = dbContext.Students
        .Where(s => s.ClassId == requestedClass)
        .Select(s => $"{s.FirstName} {s.LastName}")
        .ToList();
        if (students.Count > 0)
        {
            Console.WriteLine($"Displaying students from class {requestedClass}");
        }
        else
        {
            Console.WriteLine("No students found. Try again");
        }
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    internal static void CreateNewStudent(SchoolContext dbContext)
    {
        string firstName = GetUserInput.ValidateString("Enter Student first name:");
        string lastname = GetUserInput.ValidateString("Enter Student last name:");

        string personalNumber = GetUserInput.ValidateString("Enter Student personal number:");
        int classId = GetUserInput.ValidateInt("Enter Student class id:");


        Console.WriteLine("Enter Student class ID:");
        Student newStudent = new()
        {
            FirstName = firstName,
            LastName = lastname,
            PersonalNumber = personalNumber,
            ClassId = classId
        };

        dbContext.Students.Add(newStudent);
        dbContext.SaveChanges();
    }
    internal static void GetAllStaff(SchoolContext dbContext)
    {
        var allStaff = dbContext.Staff
        .Select(s => $"{s.FirstName} {s.LastName}")
        .ToList();

        foreach (var staff in allStaff)
        {
            Console.WriteLine(staff);
        }
    }
}