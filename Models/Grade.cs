using System;
using System.Collections.Generic;

namespace ErikLabb3.Models;

public partial class Grade
{
    public string Grade1 { get; set; } = null!;

    public virtual ICollection<StudentCourseRecord> StudentCourseRecords { get; set; } = new List<StudentCourseRecord>();
}
