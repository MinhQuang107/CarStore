using CarStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CarStore.Areas.Admin.Models;
using System.Diagnostics;

namespace CarStore.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private IPasswordHasher<AppUser> _passwordHasher;

        private readonly CarStoreContext _carStore;

        public UsersController(RoleManager<IdentityRole<string>> roleManager, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHash, CarStoreContext carStore)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordHasher = passwordHash;
            _carStore = carStore;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users;
            var managers = _carStore.Managers.ToList();
            return View(managers);
        }

        public async Task<IActionResult> List()
        {
            var users = _userManager.Users;
            IList<AppUserModel> list = new List<AppUserModel>();
            foreach (var user in users)
            {
                list.Add(new AppUserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = await GetUserRoles(user),
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                });
            }
            return View(list);
        }

        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        //    [Authorize(Roles = "SUPERADMIN")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            AppUserModel m = new AppUserModel();
            if (id != "0")
            {
                AppUser user = new AppUser();
                user = await _userManager.FindByIdAsync(id);
                IList<string> rol = await _userManager.GetRolesAsync(user);
                m.Id = user.Id;
                m.Roles = rol;
                m.FullName = user.FullName;
                m.Email = user.Email;
                m.UserName = user.UserName;
            }
            //Get all Roles
            IList<AppRoleModel> roles = new List<AppRoleModel>();
            foreach (var role in _roleManager.Roles)
            {
                roles.Add(new AppRoleModel { RoleId = role.Id, RoleName = role.Name });
            }
            ViewData["Roles"] = roles;
            return View(m);
        }


        //    [Authorize(Roles = "SUPERADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, string userName, string fullName, string email, string? password, string[] roleName)
        {
            //string keyValue = Format.GetNullString(formRequest["keyValue"], "0");

            //string message = string.Empty;
            //string messageType = DisplayMessage.TypeSuccess;
            string[] rolesName = roleName;

            IdentityResult result;
            try
            {
                AppUser appUser = await _userManager.FindByIdAsync(Id);
                if (appUser == null)
                    appUser = new AppUser();
                appUser.UserName = userName;
                appUser.Email = email;
                appUser.FullName = fullName;

                //if (Id == "0")
                //{
                //    result = await userManager.CreateAsync(appUser, Format.GetNullString(formRequest["password"], string.Empty));
                //    if (rolesName.Length > 0)
                //    {
                //        foreach (var r in rolesName)
                //        {
                //            await userManager.AddToRoleAsync(appUser, r);
                //        }
                //    }

                //}

                if (!string.IsNullOrEmpty(password))
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, password);

                result = await _userManager.UpdateAsync(appUser);

                //string[] allRoles = _roleManager.Roles.Select(x => x.Name).ToArray();

                var userRoles = await _userManager.GetRolesAsync(appUser);  //get all curent roles

                if (rolesName.Length > 0)
                {
                    await _userManager.RemoveFromRolesAsync(appUser, userRoles); //remove all deprecated roles
                                                                                //update new roles
                    foreach (var r in rolesName)
                    {
                        await _userManager.AddToRoleAsync(appUser, r);
                    }
                }
                if (result.Succeeded)
                {
                    //message = LanguageUtils.getString("message_success");
                    return RedirectToAction("List");
                }
                else
                {
                    //message = LanguageUtils.getString("message_alert");
                    //messageType = DisplayMessage.TypeError;
                    return View();
                }
                //return Json(new { Type = messageType, Message = message, Title = LanguageUtils.getString("message_alert"), CallBack = "siteView.search();" });
            }
            catch (Exception ex)
            {
                //return Json(new { Type = DisplayMessage.TypeError, Message = ex.StackTrace, Title = LanguageUtils.getString("message_alert"), CallBack = "" }
                //);
                return View();
            }
        }
        //    private void Errors(IdentityResult result)
        //    {
        //        foreach (IdentityError error in result.Errors)
        //            ModelState.AddModelError("", error.Description);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Delete(string id)
        //    {
        //        AppUser user = await userManager.FindByIdAsync(id);
        //        if (user != null)
        //        {
        //            IdentityResult result = await userManager.DeleteAsync(user);
        //            if (result.Succeeded)
        //                return RedirectToAction("Index");
        //            else
        //                Errors(result);
        //        }
        //        else
        //            ModelState.AddModelError("", "User Not Found");
        //        return View("Index", userManager.Users);
        //    }
        //    public ViewResult Create() => View();
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AspNetUsers user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.PasswordHash);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        //[HttpPost]
        //public IActionResult Create(IdentityRole role)
        //{
        //    if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
        //    {
        //        _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
