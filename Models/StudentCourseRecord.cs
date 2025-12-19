using System;
using System.Collections.Generic;

namespace ErikLabb3.Models;

public partial class StudentCourseRecord
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public string? StudentFinalGrade { get; set; }

    public int? GradedBy { get; set; }

    public DateOnly GradeSetDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Staff? GradedByNavigation { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Grade? StudentFinalGradeNavigation { get; set; }
}
