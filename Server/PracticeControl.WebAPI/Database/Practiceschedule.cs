using System;
using System.Collections.Generic;

namespace PracticeControl.WebAPI.Database;

public partial class Practiceschedule
{
    public long Id { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public long IdEmployee { get; set; }

    public long IdGroup { get; set; }

    public bool Isdeleted { get; set; }

    public long? IdPractice { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual Practice? IdPracticeNavigation { get; set; }
}
