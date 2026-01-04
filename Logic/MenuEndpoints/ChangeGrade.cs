using System.Transactions;
using BankApp.HelperClasses;
using ErikLabb4.Models;
using Microsoft.EntityFrameworkCore;

class ChangeGrade
{
    internal static void ChangeGradeByClass(SchoolContext dbContext)
    {
        while (true)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();


            var listOfClasseIds = dbContext.Classes.Select(c => c.ClassId).ToList();

            int classChoice = ValidateClass("Enter the ID to the class of the student whos grades you want to change", listOfClasseIds);

            int studentChoice = ValidateStudent("Enter the Id of the student", classChoice, dbContext);

            int recordChoice = ValidateRecord("Enter the ID of the Course you want to change grade for:", studentChoice, dbContext);

            StudentCourseRecord record = dbContext.StudentCourseRecords.Single(r => r.Id == recordChoice);

            string newGrade = ValidateGrade("Enter the new grade", record);

            record.StudentFinalGrade = newGrade;

            if (newGrade != "0")
            {
                int newGradedBy = ValidateTeacher("Enter the ID of the teacher who set this grade:", dbContext);
                record.GradedBy = newGradedBy;
            }
            else
            {
                record.GradedBy = null;
                record.GradedBy = null;
            }

            dbContext.SaveChanges();
            dbTransaction.Commit();
            
            Console.WriteLine("Success");
            return;
        }
    }

    private static int ValidateTeacher(string prompt, SchoolContext dbContext)
    {
        var staffList = dbContext.Staff
            .Where(s => s.Department != 5) //makes sure the staff is eligable to grade
            .Select(s => new { s.FirstName, s.LastName, s.StaffId })
            .ToList();

        var listOfRecordIds = staffList.Select(r => r.StaffId);

        while (true)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Availible ID:\n");
            foreach (var staff in staffList)
            {
                Console.WriteLine($"{staff.FirstName} {staff.LastName} ID: {staff.StaffId}");
            }

            if (int.TryParse(Console.ReadLine(), out int userNumber) && listOfRecordIds.Contains(userNumber))
            {
                return userNumber;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    public static int ValidateClass(string prompt, List<int> allowedInputs)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Availible ID:\n");
            foreach (int number in allowedInputs)
            {
                Console.Write(number + ", ");
            }

            if (int.TryParse(Console.ReadLine(), out int userNumber) && allowedInputs.Contains(userNumber))
            {
                return userNumber;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
    public static int ValidateStudent(string prompt, int choice, SchoolContext dbContext)
    {
        var listOfStudentNames = dbContext.Classes
            .Where(c => c.ClassId == choice)
            .Include(c => c.Students)
            .SelectMany(c => c.Students.Select(s => new { s.FirstName, s.LastName, s.StudentId }))
            .ToList();

        List<int> allowedInputs = dbContext.Classes
            .Where(c => c.ClassId == choice)
            .Include(c => c.Students)
            .SelectMany(c => c.Students.Select(s => s.StudentId))
            .ToList();

        while (true)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Availible ID:\n");
            foreach (var student in listOfStudentNames)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.StudentId}");
            }

            if (int.TryParse(Console.ReadLine(), out int userNumber) && allowedInputs.Contains(userNumber))
            {
                return userNumber;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    internal static int ValidateRecord(string prompt, int studentChoice, SchoolContext dbContext)
    {
        var listOfRecordNamesAndId = dbContext.StudentCourseRecords
        .Where(r => r.StudentId == studentChoice).Include(r => r.Course);

        var listOfRecordIds = listOfRecordNamesAndId.Select(r => r.Id);

        while (true)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Availible IDs:\n");
            Console.WriteLine("{0,-25} {1,-20} {2,-10}", "Course Name", "Current Grade", "ID");

            foreach (var record in listOfRecordNamesAndId)
            {
                Console.WriteLine("{0,-25} {1,-20} {2,-10}", record.Course.CourseName, record.StudentFinalGrade, record.Id);
            }

            if (int.TryParse(Console.ReadLine(), out int userNumber) && listOfRecordIds.Contains(userNumber))
            {
                return userNumber;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
    internal static string ValidateGrade(string Prompt, StudentCourseRecord record)
    {
        while (true)
        {
            Console.WriteLine(Prompt);
            Console.WriteLine("Accepted Inputs:\n");
            Console.WriteLine("A, B, C, D, E, F, OR 0 (sets the grade to null)");

            List<char> acceptedInputs = new()
            {
                'A', 'B', 'C', 'D', 'E', 'F', '0'
            };

            if (char.TryParse(Console.ReadLine(), out char grade) && acceptedInputs.Contains(char.ToUpper(grade)))
            {
                return grade.ToString();
            }
            else
            {
                Console.WriteLine("Invalid try again");
            }

        }
    }

}