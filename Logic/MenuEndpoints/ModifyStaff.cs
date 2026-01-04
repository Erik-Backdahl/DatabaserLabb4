using BankApp.HelperClasses;
using ErikLabb4.Models;
using Microsoft.EntityFrameworkCore;

class ModifyStaff
{
    internal static void CreateNewStaff(SchoolContext dbContext)
    {
        string firstName = GetUserInput.ValidateString("Enter Staff first name:");
        string lastName = GetUserInput.ValidateString("Enter Staff last name:");
        string title = GetUserInput.ValidateString("Enter Staff title:");

        Staff newStaff = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Title = title
        };
        dbContext.Staff.Add(newStaff);
        dbContext.SaveChanges();
    }
    internal static void DisplayStaffByDepartment(SchoolContext dbContext)
    {
        var allDepartments = dbContext.Departments.Include(d => d.Staff);

        foreach (Department department in allDepartments)
        {
            Console.WriteLine(department.DepartmentName + "\n");

            department.Staff
                .ToList()
                .ForEach(staff => Console.WriteLine($"{staff.FirstName} {staff.LastName}"));

            Console.WriteLine();
        }
    }
}
