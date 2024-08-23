using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Exterior
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    [StringLength(255)]
    public string? High_beam { get; set; }

    [StringLength(255)]
    public string? Low_beam { get; set; }

    [StringLength(255)]
    public string? Daytime_running_lights { get; set; }

    [StringLength(255)]
    public string? Tail_lights { get; set; }

    [StringLength(255)]
    public string? Rearview_mirrors { get; set; }

    [StringLength(255)]
    public string? Rain_sensing_wipers { get; set; }

    [StringLength(255)]
    public string? Power_trunk_lid { get; set; }

    [StringLength(255)]
    public string? Hands_free_trunk_opening { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Exterior")]
    public virtual Car? Car { get; set; }
}
