using System;
using System.Collections.Generic;

namespace Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeFirstName { get; set; }

    public string? EmployeeLastName { get; set; }

    public string? Mail { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
