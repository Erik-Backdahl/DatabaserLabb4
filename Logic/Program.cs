using System;
using BankApp.HelperClasses;
using ErikLabb4.Models;
using Microsoft.Identity.Client;

namespace ErikLabb4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new SchoolContext();
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- School Management System ---");
                Console.WriteLine("1. Get All Students");
                Console.WriteLine("2. Get Students from Class");
                Console.WriteLine("3. Create New Student");
                Console.WriteLine("4. Get All Staff");
                Console.WriteLine("5. Create New Staff");
                Console.WriteLine("6. Show staff by department ----- NEW");
                Console.WriteLine("7. Show student information ----- NEW");
                Console.WriteLine("8. Show Active Courses ----- NEW");
                Console.WriteLine("9. Change student grade ----- NEW");
                Console.WriteLine("0. Exit");


                string choice = GetUserInput.ValidateString("Select an option");

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        ModifyStudents.GetAllStudents(dbContext);
                        break;
                    case "2":
                        Console.Clear();
                        ModifyStudents.GetAllStudentsFromClass(dbContext);
                        break;
                    case "3":
                        Console.Clear();
                        ModifyStudents.CreateNewStudent(dbContext);
                        break;
                    case "4":
                        Console.Clear();
                        ModifyStudents.GetAllStaff(dbContext);
                        break;
                    case "5":
                        Console.Clear();
                        ModifyStaff.CreateNewStaff(dbContext);
                        break;
                    case "6":
                        Console.Clear();
                        ModifyStaff.DisplayStaffByDepartment(dbContext);
                        break;
                    case "7":
                        Console.Clear();
                        ShowAllStudentsInfo.Entry(dbContext);
                        break;
                    case "8":
                        Console.Clear();
                        ModifyCourses.ShowCourseActivity(dbContext);
                        break;
                    case "9":
                        Console.Clear();
                        ChangeGrade.ChangeGradeByClass(dbContext);
                        break;

                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                if (running)
                {
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadLine();
                }

            }
        }
    }
}