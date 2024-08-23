using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Models;

[PrimaryKey("UserId", "RoleId")]
[Index("RoleId", Name = "IX_AspNetUserRoles_RoleId")]
public partial class AspNetUserRoles
{
    [Key]
    public string UserId { get; set; } = null!;

    [Key]
    public string RoleId { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetRoles Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetUsers User { get; set; } = null!;
}
