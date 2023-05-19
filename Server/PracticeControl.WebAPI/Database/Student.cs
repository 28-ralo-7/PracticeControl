using System;
using System.Collections.Generic;

namespace PracticeControl.WebAPI.Database;

public partial class Student
{
    public long Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public long IdGroup { get; set; }

    public string Login { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public byte[]? Passwordsalt { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Group IdGroupNavigation { get; set; } = null!;
}
