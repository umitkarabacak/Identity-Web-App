using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAPp.Contexts;

namespace WebAPp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(ILogger<IndexModel> logger
            , UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , SignInManager<AppUser> signInManager)
        {
            _logger = logger;

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            var emailAddress = "krbckumit@gmail.com";
            var password = "P@55w0rd!";
            var roleStandart = "Standart";
            var roleSupplier = "Supplier";
            var roleAdmin = "Admin";

            var userUmit = new AppUser
            {
                UserName = emailAddress,
                Email = emailAddress
            };

            var users = await _userManager.Users.ToListAsync();
            if (!users.Any())
            {
                var userCreatedResult = await _userManager.CreateAsync(userUmit);

                if (!userCreatedResult.Succeeded)
                    return;

                var setPasswordResult = await _userManager.AddPasswordAsync(userUmit, password);
                if (!setPasswordResult.Succeeded)
                    return;
            }

            var roles = await _roleManager.Roles.ToListAsync();
            if (!roles.Any())
            {
                var standartRole = new AppRole() { Name = roleStandart};
                var supplierRole = new AppRole() { Name = roleSupplier};
                var adminRole = new AppRole() { Name = roleAdmin };

                var standartRoleCreatedResult = await _roleManager.CreateAsync(standartRole);
                if (!standartRoleCreatedResult.Succeeded)
                    return;

                var supplierRoleCreatedResult = await _roleManager.CreateAsync(supplierRole);
                if (!supplierRoleCreatedResult.Succeeded)
                    return;

                var adminRoleCreatedResult = await _roleManager.CreateAsync(adminRole);
                if (!adminRoleCreatedResult.Succeeded)
                    return;
            }
        }
    }
}