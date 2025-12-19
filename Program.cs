using System;
using BankApp.HelperClasses;
using ErikLabb3.Data;
using ErikLabb3.Models;
using Microsoft.Identity.Client;

namespace ErikLabb3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new SchoolContext();
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- School Management System ---");
                Console.WriteLine("1. Get All Students");
                Console.WriteLine("2. Get Students from Class");
                Console.WriteLine("3. Create New Student");
                Console.WriteLine("4. Get All Staff");
                Console.WriteLine("5. Create New Staff");
                Console.WriteLine("6. Exit");


                string choice = GetUserInput.ValidateString("Select an option");

                switch (choice)
                {
                    case "1":
                        GetAllStudents(context);
                        break;
                    case "2":
                        GetAllStudentsFromClass(context);
                        break;
                    case "3":
                        CreateNewStudent(context);
                        break;
                    case "4":
                        GetAllStaff(context);
                        break;
                    case "5":
                        CreateNewStaff(context);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                if (running)
                {
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey(true);
                }

            }
        }

        private static void GetAllStudents(SchoolContext context)
        {
            var students = context.Students
            .Select(s => s.FirstName)
            .ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        private static void GetAllStudentsFromClass(SchoolContext context)
        {
            int requestedClass = GetUserInput.ValidateInt("Enter class ID: ");

            var students = context.Students
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

        private static void CreateNewStudent(SchoolContext context)
        {
            string firstName = GetUserInput.ValidateString("Enter Student first name:");
            string lastname = GetUserInput.ValidateString("Enter Student last name:");

            int personalNumber = GetUserInput.ValidateInt("Enter Student personal number:");
            int classId = GetUserInput.ValidateInt("Enter Student class id:");


            Console.WriteLine("Enter Student class ID:");
            Student newStudent = new()
            {
                FirstName = firstName,
                LastName = lastname,
                PersonalNumber = personalNumber,
                ClassId = classId
            };

            context.Students.Add(newStudent);
            context.SaveChanges();
        }
        private static void GetAllStaff(SchoolContext context)
        {
            var allStaff = context.Staff
            .Select(s => $"{s.FirstName} {s.LastName}")
            .ToList();

            foreach (var staff in allStaff)
            {
                Console.WriteLine(staff);
            }
        }
        private static void CreateNewStaff(SchoolContext context)
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
            context.Staff.Add(newStaff);
            context.SaveChanges();
        }
    }
}