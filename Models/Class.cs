using System;
using System.Collections.Generic;

namespace ErikLabb3.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int HomeTeacherId { get; set; }

    public string HomeRoom { get; set; } = null!;

    public int ClassSize { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Classroom HomeRoomNavigation { get; set; } = null!;

    public virtual Staff HomeTeacher { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
