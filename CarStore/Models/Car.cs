using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CarStore.Models;

public partial class Car
{
    [Key]
    public int ID { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Body { get; set; }

    [StringLength(50)]
    public string? Fuel_Type { get; set; }

    [StringLength(50)]
    public string? Transmission { get; set; }

    [StringLength(50)]
    public string? Drive { get; set; }

    [StringLength(255)]
    public string? Manufacturer { get; set; }

    [StringLength(255)]
    public string? Model { get; set; }

    public int? Year { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    [StringLength(255)]
    public string? Engine_Type { get; set; }

    public double? Engine_Displacement { get; set; }

    public int? Engine_Power { get; set; }

    public int? Engine_Torque { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public LocalDate? Production_Date { get; set; }

    [StringLength(50)]
    public string? VIN { get; set; }

    public int? Mileage { get; set; }

    [StringLength(50)]
    public string? Car_Condition { get; set; }

    [StringLength(255)]
    public string? Warranty { get; set; }

    public string? Features { get; set; }

    public string? Options { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<Engine_Transmission> Engine_Transmission { get; set; } = new List<Engine_Transmission>();

    [InverseProperty("Car")]
    public virtual ICollection<Exterior> Exterior { get; set; } = new List<Exterior>();

    [InverseProperty("Car")]
    public virtual ICollection<ImageCar> ImageCar { get; set; } = new List<ImageCar>();

    [InverseProperty("Car")]
    public virtual ICollection<Interior_Convenience> Interior_Convenience { get; set; } = new List<Interior_Convenience>();

    [InverseProperty("Car")]
    public virtual ICollection<Safety> Safety { get; set; } = new List<Safety>();

    [InverseProperty("Car")]
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();

    [InverseProperty("Car")]
    public virtual ICollection<Size_Weight> Size_Weight { get; set; } = new List<Size_Weight>();
}
