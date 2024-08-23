using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Employees
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

    [StringLength(450)]
    public string? User_ID { get; set; }

    [InverseProperty("IDNavigation")]
    public virtual Accountants? Accountants { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Appointment> Appointment { get; set; } = new List<Appointment>();

    [InverseProperty("IDNavigation")]
    public virtual Maintenance_Staff? Maintenance_Staff { get; set; }

    [InverseProperty("Salesperson")]
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();

    [InverseProperty("IDNavigation")]
    public virtual Salespeople? Salespeople { get; set; }

    [InverseProperty("IDNavigation")]
    public virtual Security? Security { get; set; }

    [ForeignKey("User_ID")]
    [InverseProperty("Employees")]
    public virtual AspNetUsers? User { get; set; }
}
