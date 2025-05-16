using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Models
{
    public class WaslaSecurityContext : IdentityDbContext
    {

        public WaslaSecurityContext(DbContextOptions<WaslaSecurityContext> options) : base(options) { }
        public DbSet<Wasla.Models.RegisterViewModel> RegisterViewModel { get; set; } = default!;
        public DbSet<Wasla.Models.LoginViewModel> LoginViewModel { get; set; } = default!;



    }
}
