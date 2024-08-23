using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CarStore.Models;

public partial class Sales
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    public int? Customer_ID { get; set; }

    public int? Salesperson_ID { get; set; }

    public LocalDate? Sale_Date { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Sale_Price { get; set; }

    [StringLength(50)]
    public string? Payment_Method { get; set; }

    [StringLength(255)]
    public string? Invoice_Number { get; set; }

    [StringLength(255)]
    public string? Warranty { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Sales")]
    public virtual Car? Car { get; set; }

    [ForeignKey("Customer_ID")]
    [InverseProperty("Sales")]
    public virtual Customers? Customer { get; set; }

    [ForeignKey("Salesperson_ID")]
    [InverseProperty("Sales")]
    public virtual Employees? Salesperson { get; set; }
}
