using System;
using System.Collections.Generic;

namespace Entities;

public partial class Resident
{
    public int ResidentId { get; set; }

    public string? ResidentFirstName { get; set; }

    public string? ResidentLastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? HeNeedSomeMilk { get; set; }

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
