using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Safety
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    [StringLength(255)]
    public string? Airbags { get; set; }

    [StringLength(255)]
    public string? ABS_BAS_brakes { get; set; }

    [StringLength(255)]
    public string? Electronic_stability_control { get; set; }

    [StringLength(255)]
    public string? Vehicle_body_stability { get; set; }

    [StringLength(255)]
    public string? Electronic_traction_control { get; set; }

    [StringLength(255)]
    public string? Traction_control_during_acceleration { get; set; }

    [StringLength(255)]
    public string? Crosswind_stability { get; set; }

    [StringLength(255)]
    public string? Hill_start_assist { get; set; }

    [StringLength(255)]
    public string? Hill_descent_assist { get; set; }

    [StringLength(255)]
    public string? Active_parking_assistance { get; set; }

    [StringLength(255)]
    public string? Automatic_protection { get; set; }

    [StringLength(255)]
    public string? Attention_assist { get; set; }

    [StringLength(255)]
    public string? Opt_360_degree_camera { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Safety")]
    public virtual Car? Car { get; set; }
}
