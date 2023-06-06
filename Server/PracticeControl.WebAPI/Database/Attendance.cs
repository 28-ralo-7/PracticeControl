using System;
using System.Collections.Generic;

namespace PracticeControl.WebAPI.Database;

public partial class Attendance
{
    public long Id { get; set; }

    public long IdStudent { get; set; }

    public DateOnly Date { get; set; }

    public bool Ispresent { get; set; }

    public bool Isdeleted { get; set; }

    public long IdPractice { get; set; }

    public byte[]? Photo { get; set; }

    public string? Comment { get; set; }

    public virtual Practiceschedule IdPracticeNavigation { get; set; } = null!;

    public virtual Student IdStudentNavigation { get; set; } = null!;
}
