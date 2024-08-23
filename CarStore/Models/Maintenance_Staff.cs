using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Maintenance_Staff
{
    [Key]
    public int ID { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Salary { get; set; }

    public double? Working_Hours { get; set; }

    [StringLength(255)]
    public string? Specialization { get; set; }

    [ForeignKey("ID")]
    [InverseProperty("Maintenance_Staff")]
    public virtual Employees IDNavigation { get; set; } = null!;
}
