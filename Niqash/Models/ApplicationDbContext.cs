using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Niqash.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Project classes
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Love> Loves { get; set; }

        public DbSet<Follow> Follows { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}