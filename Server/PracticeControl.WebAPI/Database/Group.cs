using System;
using System.Collections.Generic;

namespace PracticeControl.WebAPI.Database;

public partial class Group
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<Practiceschedule> Practiceschedules { get; set; } = new List<Practiceschedule>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
