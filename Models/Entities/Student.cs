using System;
using System.Collections.Generic;

namespace StudentManagementDemo.Models.Entities;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? DateOfBirth { get; set; }

    public bool? Gender { get; set; }

    public double? Score { get; set; }
}
