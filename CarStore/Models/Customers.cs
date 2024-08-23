using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Customers
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

    [StringLength(50)]
    public string? Referral_Status { get; set; }

    public int? Discount_Level { get; set; }

    [StringLength(450)]
    public string? User_ID { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Appointment> Appointment { get; set; } = new List<Appointment>();

    [InverseProperty("Customer")]
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();

    [ForeignKey("User_ID")]
    [InverseProperty("Customers")]
    public virtual AspNetUsers? User { get; set; }
}
