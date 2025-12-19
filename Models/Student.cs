using System;
using System.Collections.Generic;

namespace ErikLabb3.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int PersonalNumber { get; set; }

    public int ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<StudentCourseRecord> StudentCourseRecords { get; set; } = new List<StudentCourseRecord>();
}
