using System;
using System.Collections.Generic;

namespace ErikLabb4.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int MainTeacherId { get; set; }

    public string MainClassroomId { get; set; } = null!;

    public virtual Classroom MainClassroom { get; set; } = null!;

    public virtual Staff MainTeacher { get; set; } = null!;

    public virtual ICollection<StudentCourseRecord> StudentCourseRecords { get; set; } = new List<StudentCourseRecord>();
}
