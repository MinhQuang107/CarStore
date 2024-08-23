using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace CarStore.Areas.Admin.Models
{
    public class AppRoleModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
