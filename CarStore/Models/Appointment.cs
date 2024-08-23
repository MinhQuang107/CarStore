using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CarStore.Models;

public partial class Appointment
{
    [Key]
    public int ID { get; set; }

    public int? Employee_ID { get; set; }

    public int? Customer_ID { get; set; }

    [StringLength(50)]
    public string? Car_Type { get; set; }

    [StringLength(255)]
    public string? Car_Model { get; set; }

    [StringLength(50)]
    public string? Purpose { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Budget { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    public string? Note { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Reminder { get; set; }

    public string? Feedback { get; set; }

    public string? Follow_up_Appointment { get; set; }

    [ForeignKey("Customer_ID")]
    [InverseProperty("Appointment")]
    public virtual Customers? Customer { get; set; }

    [ForeignKey("Employee_ID")]
    [InverseProperty("Appointment")]
    public virtual Employees? Employee { get; set; }
}
