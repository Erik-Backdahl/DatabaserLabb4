using System;
using System.Collections.Generic;

namespace ErikLabb3.Models;

public partial class Classroom
{
    public string RoomId { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
