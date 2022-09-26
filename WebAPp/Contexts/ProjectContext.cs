using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPp.Contexts
{
    public class ProjectContext : IdentityDbContext<AppUser, AppRole, Guid>
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

}
