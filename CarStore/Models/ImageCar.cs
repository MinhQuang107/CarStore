using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

public partial class ImageCar
{
    [Key]
    public int Id { get; set; }

    public int? Car_Id { get; set; }

    [StringLength(255)]
    public string? Image_URL { get; set; }

    [ForeignKey("Car_Id")]
    [InverseProperty("ImageCar")]
    public virtual Car? Car { get; set; }
}
