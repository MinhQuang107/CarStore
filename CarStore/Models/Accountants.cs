using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Accountants
{
    [Key]
    public int ID { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Salary { get; set; }

    [StringLength(255)]
    public string? Responsibilities { get; set; }

    [StringLength(255)]
    public string? Qualifications { get; set; }

    [ForeignKey("ID")]
    [InverseProperty("Accountants")]
    public virtual Employees? IDNavigation { get; set; } = null!;
}
