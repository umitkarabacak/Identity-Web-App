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
        private readonly ProjectContext _projectContext;

        public IndexModel(ILogger<IndexModel> logger
            , UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , SignInManager<AppUser> signInManager
            , ProjectContext projectContext)
        {
            _logger = logger;

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _projectContext = projectContext;
        }

        public async Task OnGet()
        {
            var emailAddress = "krbckumit@gmail.com";
            var password = "P@55w0rd!";
            var roleStandart = "Standart";
            var roleSupplier = "Supplier";
            var roleAdmin = "Admin";
            var allRoleNames = new string[] { roleStandart, roleAdmin, roleSupplier };

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
                var standartRole = new AppRole() { Name = roleStandart };
                var supplierRole = new AppRole() { Name = roleSupplier };
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

            var user = await _userManager.FindByEmailAsync(emailAddress);
            if (user is null)
                return;

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                var adminRole = await _roleManager.FindByNameAsync(roleAdmin);
                var adminRoleInserRequest = new UserRole
                {
                    UserId = user.Id,
                    RoleId = adminRole.Id,
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddMonths(1)
                };
                await _projectContext.UserRoles.AddAsync(adminRoleInserRequest);
                var createdAdminRoleResult = await _projectContext.SaveChangesAsync();
                if (createdAdminRoleResult <= 0)
                    return;

                //var createAdminRolesResult = await _userManager.AddToRoleAsync(user, roleAdmin);
                //if (!createAdminRolesResult.Succeeded)
                //    return;

                //var addRolesResult = await _userManager.AddToRolesAsync(user, allRoleNames);
                //if (!addRolesResult.Succeeded)
                //    return;
            }
            userRoles = await _userManager.GetRolesAsync(user);

            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(userRoles));
        }
    }
}