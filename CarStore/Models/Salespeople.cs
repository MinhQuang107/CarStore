using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Salespeople
{
    [Key]
    public int ID { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Salary { get; set; }

    public double? Commission_Rate { get; set; }

    public int? Sales_Count { get; set; }

    public string? Achievements { get; set; }

    [ForeignKey("ID")]
    [InverseProperty("Salespeople")]
    public virtual Employees IDNavigation { get; set; } = null!;
}
