using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Niqash.Resources;

namespace Niqash.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // custom props
        [Display(Name = "firstName", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string FirstName { get; set; }

        [Display(Name = "lastName", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string LastName { get; set; }

        [Display(Name = "aboutMe", ResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(200, ErrorMessageResourceName = "max200", ErrorMessageResourceType = typeof(ModelsResource))]
        public string AboutMe { get; set; }

        public string ProfilePicSrc { get; set; }

        //[UniqueNickname]
        //[Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        //[MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource)), MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessageResourceName = "vlaidUserNameMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        //[Display(Name = "nickname", ResourceType = typeof(ModelsResource))]
        //public string Nickname { get; set; }


        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Love> Loves { get; set; }

        //public virtual ICollection<Follow> Follows{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}