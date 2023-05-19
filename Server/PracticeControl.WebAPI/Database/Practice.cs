using System;
using System.Collections.Generic;

namespace PracticeControl.WebAPI.Database;

public partial class Practice
{
    public long Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string Practicemodule { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<Practiceschedule> Practiceschedules { get; set; } = new List<Practiceschedule>();
}
