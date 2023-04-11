using System;
using System.Collections.Generic;

namespace StudentManagementDemo.Models.Entities;

public partial class Account
{
    public string? Account1 { get; set; }

    public string? Password { get; set; }

    public virtual Student? Account1Navigation { get; set; }
}
