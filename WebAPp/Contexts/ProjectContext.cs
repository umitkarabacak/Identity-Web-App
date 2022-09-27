using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPp.Contexts
{
    public class ProjectContext : IdentityDbContext<AppUser, AppRole, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }
    }

    public class AppUser : IdentityUser<Guid>
    {

    }

    public class AppRole : IdentityRole<Guid>
    {

    }

    public class UserClaim : IdentityUserClaim<Guid> { }

    public class UserRole : IdentityUserRole<Guid>
    {
        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }
    }

    public class UserLogin : IdentityUserLogin<Guid> { }

    public class RoleClaim : IdentityRoleClaim<Guid> { }

    public class UserToken : IdentityUserToken<Guid> { }
}
