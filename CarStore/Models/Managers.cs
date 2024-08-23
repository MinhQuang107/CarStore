using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CarStore.Models;

public partial class Managers
{
    [Key]
    public int ID { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(255)]
    public string? Position { get; set; }

    [StringLength(255)]
    public string? Department { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Salary { get; set; }

    [StringLength(255)]
    public string? Responsibilities { get; set; }

    public int? Experience { get; set; }

    public LocalDate? Start_Date { get; set; }

    [StringLength(450)]
    public string? User_ID { get; set; }

    [ForeignKey("User_ID")]
    [InverseProperty("Managers")]
    public virtual AspNetUsers? User { get; set; }
}
