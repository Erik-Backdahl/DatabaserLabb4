using System;
using System.Collections.Generic;

namespace ErikLabb4.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int YearlySalary { get; set; }

    public int Department { get; set; }

    public bool? Substitute { get; set; }

    public DateOnly StartDate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual ICollection<StudentCourseRecord> StudentCourseRecords { get; set; } = new List<StudentCourseRecord>();
}
