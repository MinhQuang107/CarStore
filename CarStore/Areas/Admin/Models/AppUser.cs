using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace CarStore.Areas.Admin.Models
{
    public class AppUser : IdentityUser<string>
    {
        //[Required]
        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
