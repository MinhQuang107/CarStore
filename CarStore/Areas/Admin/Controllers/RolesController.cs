using CarStore.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole<string>> _roleManager;

        public RolesController(RoleManager<IdentityRole<string>> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //Get all Roles
            //IList<AppRoleModel> roles = new List<AppRoleModel>();
            //foreach (var role in _roleManager.Roles)
            //{
            //    roles.Add(new AppRoleModel { RoleId = role.Id, RoleName = role.Name });
            //}
            //ViewData["Roles"] = roles;
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
