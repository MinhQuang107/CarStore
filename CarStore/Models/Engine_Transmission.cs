using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Engine_Transmission
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    [StringLength(255)]
    public string? Engine_type { get; set; }

    public int? Maximum_power { get; set; }

    public int? Maximum_torque { get; set; }

    [StringLength(255)]
    public string? Transmission_type { get; set; }

    [StringLength(255)]
    public string? Drivetrain { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Acceleration_0_100kmh { get; set; }

    public int? Maximum_speed_kmh { get; set; }

    [StringLength(255)]
    public string? Fuel_type { get; set; }

    [StringLength(255)]
    public string? ECO_start_stop { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Engine_Transmission")]
    public virtual Car? Car { get; set; }
}
