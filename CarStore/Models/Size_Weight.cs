using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class Size_Weight
{
    [Key]
    public int ID { get; set; }

    public int? Car_ID { get; set; }

    public int? Number_of_seats { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Length_mm { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Width_mm { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Height_mm { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Wheelbase_mm { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Curb_weight_kg { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Gross_weight_kg { get; set; }

    [ForeignKey("Car_ID")]
    [InverseProperty("Size_Weight")]
    public virtual Car? Car { get; set; }
}
