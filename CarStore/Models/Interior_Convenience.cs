using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Interior_Convenience
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    [StringLength(255)]
    public string? Interior_material { get; set; }

    [StringLength(255)]
    public string? Steering_wheel { get; set; }

    [StringLength(255)]
    public string? Multi_information_display { get; set; }

    [StringLength(255)]
    public string? Gear_shift_paddles { get; set; }

    [StringLength(255)]
    public string? Keyless_entry_system { get; set; }

    [StringLength(255)]
    public string? Cruise_Control { get; set; }

    [StringLength(255)]
    public string? Electronic_parking_brake { get; set; }

    [StringLength(255)]
    public string? Auto_dimming_rearview_mirror { get; set; }

    [StringLength(255)]
    public string? Seats { get; set; }

    [StringLength(255)]
    public string? Front_seats { get; set; }

    [StringLength(255)]
    public string? Second_row_seats { get; set; }

    [StringLength(255)]
    public string? Third_row_seats { get; set; }

    [StringLength(255)]
    public string? Automatic_climate_control { get; set; }

    [StringLength(255)]
    public string? Entertainment_screen { get; set; }

    [StringLength(255)]
    public string? Audio_system { get; set; }

    [StringLength(255)]
    public string? Touchpad { get; set; }

    [StringLength(255)]
    public string? Voice_control { get; set; }

    [StringLength(255)]
    public string? Integrated_GPS_navigation { get; set; }

    [StringLength(255)]
    public string? Apple_Carplay_Android_Auto { get; set; }

    [StringLength(255)]
    public string? Wireless_phone_charging { get; set; }

    [StringLength(255)]
    public string? Starry_sky { get; set; }

    [StringLength(255)]
    public string? Sunroof { get; set; }

    [StringLength(255)]
    public string? Soft_close_doors { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Interior_Convenience")]
    public virtual Car? Car { get; set; }
}
